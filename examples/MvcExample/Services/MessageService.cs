using Scrutor.AspNetCore;

namespace MvcExample.Services;

// Auto-registered as Transient
public class MessageService : IMessageService, ITransientLifetime
{
    public string GetMessage()
    {
        return "This service was automatically registered using Scrutor.AspNetCore with Transient lifetime!";
    }
}
