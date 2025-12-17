namespace BlazorServerExample.Data;

/// <summary>
/// Provides weather forecast data generation services.
/// </summary>
public class WeatherForecastService {
    /// <summary>
    /// Array of possible weather condition summaries.
    /// </summary>
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    /// <summary>
    /// Generates weather forecasts for the next 5 days starting from the specified date.
    /// </summary>
    /// <param name="startDate">The starting date for the forecast.</param>
    /// <returns>An array of weather forecasts.</returns>
    public Task<WeatherForecast[]> GetForecastAsync(DateOnly startDate) {
        return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast {
            Date = startDate.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        }).ToArray());
    }
}
