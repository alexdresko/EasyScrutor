# EasyScrutor

Convention-based dependency injection for .NET using Scrutor. Automatically register your services by implementing simple marker interfaces - no manual registration needed.

## Installation

```bash
dotnet add package EasyScrutor
```

## Quick Start

**1. Mark your services with lifetime interfaces:**

```csharp
using EasyScrutor;

public interface IMyService { }

// Automatically registered as Scoped
public class MyService : IMyService, IScopedLifetime { }
```

**2. Add to your application:**

```csharp
builder.Services.AddEasyScrutor();
```

**3. Use your services:**

```csharp
public class MyController
{
    public MyController(IMyService myService) { }
}
```

## Lifetime Interfaces

- `IScopedLifetime` - Scoped registration
- `ITransientLifetime` - Transient registration
- `ISingletonLifetime` - Singleton registration
- `ISelfScopedLifetime` - Self-registration (Scoped)
- `ISelfTransientLifetime` - Self-registration (Transient)
- `ISelfSingletonLifetime` - Self-registration (Singleton)

## Documentation

For complete documentation, examples, and advanced usage, visit the [GitHub repository](https://github.com/alexdresko/EasyScrutor).

## License

MIT
