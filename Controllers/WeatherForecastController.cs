using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Velto.Data;

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

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IActionResult> Get()
    {
        var users = await _context.Users.ToListAsync();
        return Ok(users);
        // return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        // {
        //     Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        //     TemperatureC = Random.Shared.Next(-20, 55),
        //     Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        // })
        // .ToArray();
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
