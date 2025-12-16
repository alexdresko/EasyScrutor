using Scrutor.AspNetCore;

namespace BlazorServerExample.Services;

// Auto-registered as Singleton using Scrutor.AspNetCore
public class CounterService : ICounterService, ISingletonLifetime
{
    private int _count = 0;

    public int GetCount() => _count;

    public void Increment() => _count++;
}
