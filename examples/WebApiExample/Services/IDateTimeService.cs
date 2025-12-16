namespace WebApiExample.Services;

/// <summary>
/// Provides date and time services.
/// </summary>
public interface IDateTimeService
{
    /// <summary>
    /// Gets the current date and time in UTC.
    /// </summary>
    /// <returns>The current UTC date and time.</returns>
    DateTime GetCurrentDateTime();
}
