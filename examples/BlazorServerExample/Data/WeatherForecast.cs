namespace BlazorServerExample.Data;

/// <summary>
/// Represents a weather forecast for a specific date.
/// </summary>
public class WeatherForecast {
    /// <summary>
    /// Gets or sets the date of the forecast.
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// Gets or sets the temperature in Celsius.
    /// </summary>
    public int TemperatureC { get; set; }

    /// <summary>
    /// Gets the temperature in Fahrenheit, calculated from the Celsius temperature.
    /// </summary>
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    /// <summary>
    /// Gets or sets a textual summary of the weather conditions.
    /// </summary>
    public string? Summary { get; set; }
}