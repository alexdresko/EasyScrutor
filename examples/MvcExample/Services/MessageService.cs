using EasyScrutor;

namespace MvcExample.Services;

/// <summary>
/// Provides message generation with transient lifetime.
/// Auto-registered as Transient using EasyScrutor.
/// </summary>
public class MessageService : IMessageService, ITransientLifetime
{
    /// <summary>
    /// Gets a message string about the service registration.
    /// </summary>
    /// <returns>A message string.</returns>
    public string GetMessage()
    {
        return "This service was automatically registered using EasyScrutor with Transient lifetime!";
    }
}
