using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Models;
using api.Dtos;
using api.Data;
using api.Utils;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using api.Services;

namespace api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly VeltoContext _context;
    private readonly TokenService _tokenService;

    public AuthController(VeltoContext context,TokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

   [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
    {      
        var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == request.Email);
        if (user == null || !Helper.VerifyPassword(request.Password, user.PasswordHash))
        {
            return Unauthorized(new { message = "Invalid credentials" });
        }
        var userDto = new UserDto
        {
            Id = user.Id,
            TenantId = user.TenantId,
            Email = user.Email
        };
        var token = _tokenService.GenerateToken(user);
        return Ok(new LoginResponse { Token = token, User = userDto});
    }
 
    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUser()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
        {
            return Unauthorized();
        }

        var user = await _context.Users
            .Include(u => u.TenantId)
            .FirstOrDefaultAsync(u => u.Id.ToString() == userId);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }
}

// Compare this snippet from Controllers/AccountController.cs: