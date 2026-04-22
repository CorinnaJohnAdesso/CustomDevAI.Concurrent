using OpenMeteo;

namespace CustomDevAI.SimpleAgent.Services;

public interface IWeatherLookupService
{
    Task<WeatherForecast?> GetByLocation(string location, string iso_start_date);
}