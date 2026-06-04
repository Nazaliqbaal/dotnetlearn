using Microsoft.AspNetCore.Mvc;
using dotnet_learn.Services;

namespace dotnet_learn.Controllers
{
    [ApiController]
    [Route("weather")]
    public class WeatherController(IWeatherService weatherService) : ControllerBase
    {
        [HttpGet("{city}")]
        public async Task<IActionResult> GetCurrent(string city, CancellationToken ct)
        {
            var result = await weatherService.GetCurrentWeatherAsync(city, ct);
            if (result is null)
                return NotFound(new { message = $"City '{city}' not found." });

            return Ok(result);
        }

        [HttpGet("{city}/forecast")]
        public async Task<IActionResult> GetForecast(string city, CancellationToken ct)
        {
            var result = await weatherService.GetForecastAsync(city, ct);
            if (result is null)
                return NotFound(new { message = $"City '{city}' not found." });

            return Ok(result);
        }
    }
}
