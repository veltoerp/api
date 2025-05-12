using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Models;
using api.Dtos;
using api.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace api.Controllers;

public class AuthController : ControllerBase
{
    private readonly VeltoContext _context;

    public AuthController(VeltoContext context)
    {
        _context = context;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _context.Users
            .Include(u => u.TenantId)
            .FirstOrDefaultAsync(u => u.Email == request.Email && u.PasswordHash == request.Password);

        if (user == null)
        {
            return Unauthorized();
        }

        // var claims = new List<Claim>
        // {
        //     new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        //     new Claim(ClaimTypes.Name, user.Name),
        //     new Claim("TenantId", user.TenantId.ToString())
        // };

        // var identity = new ClaimsIdentity(claims, "Custom");
        // var principal = new ClaimsPrincipal(identity);

        return Ok(new { Token = "fake-jwt-token" });
    }
    // [HttpPost("register")   ]
    // public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    // {
    //     var existingUser = await _context.Users
    //         .FirstOrDefaultAsync(u => u.Email == request.Email);

    //     if (existingUser != null)
    //     {
    //         return BadRequest("User already exists");
    //     }

    //     var user = new User
    //     {
    //         Id = Guid.NewGuid(),
    //         TenantId = request.TenantId,
    //         Email = request.Email,
    //         PasswordHash = request.Password,
    //         CreatedAt = DateTime.UtcNow
    //     };

    //     _context.Users.Add(user);
    //     await _context.SaveChangesAsync();

    //     return Ok(new { user.Id });
    // }
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