namespace BlazorServerExample.Services;

/// <summary>
/// Provides counter functionality for tracking and incrementing a count value.
/// </summary>
public interface ICounterService {
    /// <summary>
    /// Gets the current count value.
    /// </summary>
    /// <returns>The current count.</returns>
    int GetCount();

    /// <summary>
    /// Increments the counter by one.
    /// </summary>
    void Increment();
}
