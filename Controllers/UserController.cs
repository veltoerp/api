
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Models; 
using api.Dtos; 
using api.Utils; 

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly VeltoContext _context;
        private readonly ILogger<UserController> _logger;

        public UserController(VeltoContext context, ILogger<UserController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/User
        /// <summary>
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();

            var userDtos = users.Select(u => new UserDto
            {
                Id = u.Id,
                TenantId = u.TenantId, 
                Email = u.Email                
            }).ToList();

            return Ok(userDtos);
        }

        // GET: api/User/5
        /// <summary>  
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(Guid id) // ID'nin Guid olduğunu varsayalım
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            var userDto = new UserDto
            {
                Id = user.Id,
                TenantId = user.TenantId, 
                Email = user.Email
               
            };

            return Ok(userDto);
        }

        // POST: api/User
        /// <summary>
        /// Yeni bir kullanıcı oluşturur.
        /// </summary>
        /// <param name="createUserDto">Oluşturulacak kullanıcı bilgileri.</param>
        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser(CreateUserDto createUserDto)
        {      
            if (string.IsNullOrEmpty(createUserDto.Email) || string.IsNullOrEmpty(createUserDto.Password))
            {
                return BadRequest(new { message = "Email and Password are required." });
            }
            if (await _context.Users.AnyAsync(u => u.Email == createUserDto.Email))
            {
                return Conflict(new { message = "A user with this email address already exists." });
            }

            var user = new User
            {               
                Email = createUserDto.Email,
                PasswordHash = Helper.HashPassword(createUserDto.Password), 
                TenantId = createUserDto.TenantId 
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            
            var userDto = new UserDto
            {
                Id = user.Id,
                TenantId = user.TenantId,
                Email = user.Email               
            };            
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, userDto);
        }

        // PUT: api/User/5
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, UpdateUserDto updateUserDto) // ID'nin Guid olduğunu varsayalım
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
           
            user.Email = updateUserDto.Email;
            //user.TenantId = updateUserDto.TenantId; 
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw; // Farklı bir eşzamanlılık sorunuysa yeniden fırlat
                }
            }
            catch (DbUpdateException ex)
            {
                 _logger.LogError(ex, "An error occurred while updating the user with ID {UserId}.", id);
                 return StatusCode(500, "An error occurred while updating the user.");
            }
            return NoContent(); // Başarılı PUT için 204 No Content standarttır
        }

        // DELETE: api/User/5
        /// <summary>    
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id) 
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent(); // 204 No Content
        }

        private bool UserExists(Guid id) 
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}