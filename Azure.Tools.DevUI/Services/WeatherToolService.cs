using Microsoft.Extensions.AI;
using OpenMeteo;
using System.ComponentModel;

namespace CustomDevAI.SimpleAgent.Services;

public sealed class WeatherToolService : AITool
{
    private readonly IWeatherLookupService _weatherLookupService;

    public WeatherToolService(IWeatherLookupService weatherLookupService)
    {
        _weatherLookupService = weatherLookupService;
    }

    [Description("Returns the weather for the given location and day from OpenMeteo in WeatherForecast format.")]
    public async Task<WeatherForecast?> GetWeather(
        [Description("Location of the weather forecast")] string location,
        [Description("Date of the weather forecast in ISO date format yyyy-mm-dd")] string iso_start_date)
    {
        return await _weatherLookupService.GetByLocation(location, iso_start_date);
    }
}