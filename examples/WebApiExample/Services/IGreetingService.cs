namespace WebApiExample.Services;

/// <summary>
/// Provides greeting message services.
/// </summary>
public interface IGreetingService {
    /// <summary>
    /// Gets a personalized greeting message.
    /// </summary>
    /// <param name="name">The name to include in the greeting.</param>
    /// <returns>A personalized greeting message.</returns>
    string GetGreeting(string name);
}