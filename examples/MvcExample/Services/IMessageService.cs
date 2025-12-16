namespace MvcExample.Services;

/// <summary>
/// Provides message generation services.
/// </summary>
public interface IMessageService
{
    /// <summary>
    /// Gets a message string.
    /// </summary>
    /// <returns>A message string.</returns>
    string GetMessage();
}
