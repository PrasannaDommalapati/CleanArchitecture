using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers;
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
        _logger.LogTrace(1, "NLog injected into WeatherForecastController");
    }

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        _logger.LogInformation("Hello, this is the index!!");
        return Enumerable.Range(1, 25).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
    [HttpPost]
    [Route("/apple")]
    [Description("I am idfdifkjdbdi iuhdf iuvhiduhfviudiufvidf")]
    public IActionResult SaveNumber([FromBody] WeatherForecast weatherForecast)
    {
        _logger.LogInformation("Successfully saved number");

        return Ok(weatherForecast);
    }
}
