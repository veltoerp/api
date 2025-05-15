using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Utils;
namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
     private readonly VeltoContext _context;

    public WeatherForecastController(ILogger<WeatherForecastController> logger,VeltoContext context)
    {
        _context = context;
        _logger = logger;
    }
    // public WeatherForecastController(ILogger<WeatherForecastController> logger)
    // {
    //     _logger = logger;
    // }
    [HttpGet("hash")]
    public IActionResult HashPassword(string password)
    {
        var hashedPassword = Helper.HashPassword(password);
        return Ok(hashedPassword);
    }
    
    [HttpGet("forecast")]
    public async Task<IActionResult> Get()
    {
        /*
       var users = await _context.Users.ToListAsync();
        
        */
        var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == "fatihgokce07@gmail.com");
        //var users = await _context.Users.Include(u => u.Role).ToListAsync();
        return Ok(user);        
    }
    // public async Task<IActionResult> GetCurrentUser()
    // {
    //     var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    //     if (userId == null)
    //     {
    //         return Unauthorized();
    //     }

    //     var user = await _context.Users
    //         .Include(u => u.TenantId)
    //         .FirstOrDefaultAsync(u => u.Id.ToString() == userId);

    //     if (user == null)
    //     {
    //         return NotFound();
    //     }

    //     return Ok(user);
    // }
}
