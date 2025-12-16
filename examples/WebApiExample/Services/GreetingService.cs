using EasyScrutor;

namespace WebApiExample.Services;

// This service will be automatically registered as Scoped because it implements IScopedLifetime
public class GreetingService : IGreetingService, IScopedLifetime
{
    public string GetGreeting(string name)
    {
        return $"Hello, {name}! This service was auto-registered using EasyScrutor.";
    }
}
