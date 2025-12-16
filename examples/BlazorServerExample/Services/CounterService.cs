using EasyScrutor;

namespace BlazorServerExample.Services;

/// <summary>
/// Implements counter functionality with singleton lifetime.
/// Auto-registered as Singleton using EasyScrutor.
/// </summary>
public class CounterService : ICounterService, ISingletonLifetime
{
    /// <summary>
    /// The internal counter value.
    /// </summary>
    private int _count = 0;

    /// <summary>
    /// Gets the current count value.
    /// </summary>
    /// <returns>The current count.</returns>
    public int GetCount() => _count;

    /// <summary>
    /// Increments the counter by one.
    /// </summary>
    public void Increment() => _count++;
}
