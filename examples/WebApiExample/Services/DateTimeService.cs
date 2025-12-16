using EasyScrutor;

namespace WebApiExample.Services;

// This service will be automatically registered as Singleton because it implements ISingletonLifetime
public class DateTimeService : IDateTimeService, ISingletonLifetime
{
    public DateTime GetCurrentDateTime()
    {
        return DateTime.UtcNow;
    }
}
