using Microsoft.AspNetCore.Mvc;
using Web.API.Client;

namespace Web.APP.Server.Controllers;
[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherForecastClient _weatherForecastClient;

    public WeatherForecastController(IWeatherForecastClient weatherForecastClient)
    {
        _weatherForecastClient = weatherForecastClient;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> GetAsync()
    {
        return await _weatherForecastClient.GetAsync();
    }
}
