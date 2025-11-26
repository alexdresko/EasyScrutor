using Microsoft.Extensions.DependencyInjection;
using Scrutor.AspNetCore;
using System;
using System.Reflection;

namespace Microsoft.Extensions.Hosting
{
    /// <summary>
    /// Extension methods for configuring advanced dependency injection with modern host builders.
    /// </summary>
    public static class HostBuilderExtensions
    {
        /// <summary>
        /// Adds advanced dependency injection to the host builder by scanning assemblies for classes
        /// implementing lifetime marker interfaces (ISingletonLifetime, ITransientLifetime, IScopedLifetime, etc.).
        /// Works for any .NET application (console, worker, web, etc.).
        /// </summary>
        /// <param name="builder">The host application builder.</param>
        /// <returns>The host application builder for chaining.</returns>
        public static IHostApplicationBuilder AddAdvancedDependencyInjection(this IHostApplicationBuilder builder)
        {
            builder.Services.AddAdvancedDependencyInjection();
            return builder;
        }

        /// <summary>
        /// Adds advanced dependency injection to the host builder by scanning assemblies for classes
        /// implementing lifetime marker interfaces (ISingletonLifetime, ITransientLifetime, IScopedLifetime, etc.).
        /// Works for any .NET application (console, worker, web, etc.).
        /// </summary>
        /// <param name="builder">The host application builder.</param>
        /// <param name="predicate">A predicate to filter which assemblies to scan.</param>
        /// <returns>The host application builder for chaining.</returns>
        public static IHostApplicationBuilder AddAdvancedDependencyInjection(this IHostApplicationBuilder builder, Func<Assembly, bool> predicate)
        {
            builder.Services.AddAdvancedDependencyInjection(predicate);
            return builder;
        }

        /// <summary>
        /// Adds advanced dependency injection with ASP.NET Core web-specific services to the host builder.
        /// Use this for web applications that need access to HTTP context.
        /// </summary>
        /// <param name="builder">The host application builder.</param>
        /// <returns>The host application builder for chaining.</returns>
        public static IHostApplicationBuilder AddAdvancedDependencyInjectionForWeb(this IHostApplicationBuilder builder)
        {
            builder.Services.AddAdvancedDependencyInjectionForWeb();
            return builder;
        }

        /// <summary>
        /// Adds advanced dependency injection with ASP.NET Core web-specific services to the host builder.
        /// Use this for web applications that need access to HTTP context.
        /// </summary>
        /// <param name="builder">The host application builder.</param>
        /// <param name="predicate">A predicate to filter which assemblies to scan.</param>
        /// <returns>The host application builder for chaining.</returns>
        public static IHostApplicationBuilder AddAdvancedDependencyInjectionForWeb(this IHostApplicationBuilder builder, Func<Assembly, bool> predicate)
        {
            builder.Services.AddAdvancedDependencyInjectionForWeb(predicate);
            return builder;
        }
    }
}