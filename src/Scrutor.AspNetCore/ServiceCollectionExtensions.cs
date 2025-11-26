using Scrutor.AspNetCore;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Scrutor;
using System;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds advanced dependency injection by scanning assemblies for classes implementing
        /// lifetime marker interfaces (ISingletonLifetime, ITransientLifetime, IScopedLifetime, etc.).
        /// This method works for any .NET application (console, worker, web, etc.).
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The service collection for chaining.</returns>
        public static IServiceCollection AddAdvancedDependencyInjection(this IServiceCollection services)
        {
            services.Scan(scan => scan
            .FromDependencyContext(DependencyModel.DependencyContext.Default)
            .AddClassesFromInterfaces());

            return services.AddCoreServices();
        }

        /// <summary>
        /// Adds advanced dependency injection by scanning assemblies for classes implementing
        /// lifetime marker interfaces (ISingletonLifetime, ITransientLifetime, IScopedLifetime, etc.).
        /// This method works for any .NET application (console, worker, web, etc.).
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="predicate">A predicate to filter which assemblies to scan.</param>
        /// <returns>The service collection for chaining.</returns>
        public static IServiceCollection AddAdvancedDependencyInjection(this IServiceCollection services, Func<Assembly, bool> predicate)
        {
            services.Scan(scan => scan
            .FromDependencyContext(DependencyModel.DependencyContext.Default, predicate)
            .AddClassesFromInterfaces());

            return services.AddCoreServices();
        }

        /// <summary>
        /// Adds advanced dependency injection with ASP.NET Core web-specific services
        /// (HttpContextAccessor, ActionContextAccessor).
        /// Use this for web applications that need access to HTTP context.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The service collection for chaining.</returns>
        public static IServiceCollection AddAdvancedDependencyInjectionForWeb(this IServiceCollection services)
        {
            services.AddAdvancedDependencyInjection();
            return services.AddWebServices();
        }

        /// <summary>
        /// Adds advanced dependency injection with ASP.NET Core web-specific services
        /// (HttpContextAccessor, ActionContextAccessor).
        /// Use this for web applications that need access to HTTP context.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="predicate">A predicate to filter which assemblies to scan.</param>
        /// <returns>The service collection for chaining.</returns>
        public static IServiceCollection AddAdvancedDependencyInjectionForWeb(this IServiceCollection services, Func<Assembly, bool> predicate)
        {
            services.AddAdvancedDependencyInjection(predicate);
            return services.AddWebServices();
        }

        private static IImplementationTypeSelector AddClassesFromInterfaces(this IImplementationTypeSelector selector)
        {
            //singleton
            selector.AddClasses(classes => classes.AssignableTo<ISingletonLifetime>(), true)
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsMatchingInterface()
            .WithSingletonLifetime()

            .AddClasses(classes => classes.AssignableTo<ISelfSingletonLifetime>(), true)
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsSelf()
            .WithSingletonLifetime()

            //transient
            .AddClasses(classes => classes.AssignableTo<ITransientLifetime>(), true)
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsMatchingInterface()
            .WithTransientLifetime()

            .AddClasses(classes => classes.AssignableTo<ISelfTransientLifetime>(), true)
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsSelf()
            .WithTransientLifetime()

            //scoped
            .AddClasses(classes => classes.AssignableTo<IScopedLifetime>(), true)
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsMatchingInterface()
            .WithScopedLifetime()

            .AddClasses(classes => classes.AssignableTo<ISelfScopedLifetime>(), true)
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsSelf()
            .WithScopedLifetime();

            return selector;
        }

        private static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.TryAddSingleton<IDependencyContext, DependencyContext>();
            services.TryAddSingleton(services);

            return services;
        }

        private static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

            return services;
        }
    }
}