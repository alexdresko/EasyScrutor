namespace EasyScrutor;

/// <summary>
/// Marker interface for services that should be registered as themselves with a scoped lifetime.
/// Services implementing this interface will be automatically registered as their concrete type (not interface).
/// </summary>
public interface ISelfScopedLifetime {
}
