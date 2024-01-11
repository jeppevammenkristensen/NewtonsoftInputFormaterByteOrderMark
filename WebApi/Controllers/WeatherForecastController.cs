using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpPut]
    public IActionResult Put([FromBody] TestObject input)
    {
        _logger.LogInformation("Input was {@Input}", input?.Name);
        return Ok();
    }
}