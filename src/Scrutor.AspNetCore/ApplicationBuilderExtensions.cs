using Scrutor.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Extension methods for ASP.NET Core application builder.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Initializes the ServiceLocator with the application's service provider.
        /// Call this during application startup to enable the static ServiceLocator pattern.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <returns>The application builder for chaining.</returns>
        public static IApplicationBuilder UseAdvancedDependencyInjection(this IApplicationBuilder app)
        {
            var dependencyContext = app.ApplicationServices.GetRequiredService<IDependencyContext>();
            ServiceLocator.Initialize(dependencyContext);

            return app;
        }
    }
}