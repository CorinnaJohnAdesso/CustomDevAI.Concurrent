using OpenMeteo;

namespace CustomDevAI.SimpleAgent.Services;

public sealed class OpenMeteoWeatherLookupService(TimeProvider timeProvider) : IWeatherLookupService
{
    public async Task<WeatherForecast?> GetByLocation(string location, string? iso_start_date)
    {
        if(string.IsNullOrWhiteSpace(iso_start_date))
        {
            iso_start_date = timeProvider.GetLocalNow().ToString("yyyy-MM-dd");
        }

        OpenMeteoClient client = new OpenMeteoClient();
        var weatherData = await client.QueryAsync(location, new WeatherForecastOptions { Start_date = iso_start_date });

        return weatherData;
    }
}

