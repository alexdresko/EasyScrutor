using Scrutor.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.Hosting
{
    /// <summary>
    /// Extension methods for IHost to support advanced dependency injection.
    /// </summary>
    public static class HostExtensions
    {
        /// <summary>
        /// Initializes the ServiceLocator with the host's service provider.
        /// Call this during application startup to enable the static ServiceLocator pattern.
        /// Use this for non-web applications (console apps, worker services, etc.).
        /// </summary>
        /// <param name="host">The host.</param>
        /// <returns>The host for chaining.</returns>
        public static IHost UseAdvancedDependencyInjection(this IHost host)
        {
            var dependencyContext = host.Services.GetRequiredService<IDependencyContext>();
            ServiceLocator.Initialize(dependencyContext);

            return host;
        }
    }
}