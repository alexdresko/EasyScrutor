namespace EasyScrutor
{
    /// <summary>
    /// Marker interface for services that should be registered as themselves with a transient lifetime.
    /// Services implementing this interface will be automatically registered as their concrete type (not interface).
    /// </summary>
    public interface ISelfTransientLifetime
    {
    }
}
