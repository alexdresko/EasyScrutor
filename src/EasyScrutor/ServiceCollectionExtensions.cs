using EasyScrutor;
using Scrutor;
using System;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Provides extension methods for IServiceCollection to enable advanced dependency injection scanning.
/// </summary>
public static class ServiceCollectionExtensions {
    /// <summary>
    /// Scans and registers services from all assemblies in the dependency context that implement lifetime marker interfaces.
    /// Services are registered based on their implemented lifetime interfaces (ISingletonLifetime, ITransientLifetime, IScopedLifetime).
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddEasyScrutor(this IServiceCollection services) {
        services.Scan(scan => scan
        .FromDependencyContext(DependencyModel.DependencyContext.Default)
        .AddClassesFromInterfaces());

        return services;
    }

    /// <summary>
    /// Scans and registers services from assemblies matching the predicate that implement lifetime marker interfaces.
    /// Services are registered based on their implemented lifetime interfaces (ISingletonLifetime, ITransientLifetime, IScopedLifetime).
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <param name="predicate">A predicate to filter which assemblies to scan.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddEasyScrutor(this IServiceCollection services, Func<Assembly, bool> predicate) {
        services.Scan(scan => scan
        .FromDependencyContext(DependencyModel.DependencyContext.Default, predicate)
        .AddClassesFromInterfaces());

        return services;
    }

    /// <summary>
    /// Scans and registers services from assemblies whose names start with the specified prefix.
    /// Services are registered based on their implemented lifetime interfaces (ISingletonLifetime, ITransientLifetime, IScopedLifetime).
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <param name="prefix">The prefix that assembly names must start with.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddEasyScrutorForAssembliesStartingWith(this IServiceCollection services, string prefix) {
        services.Scan(scan => scan
        .FromDependencyContext(DependencyModel.DependencyContext.Default, assembly => assembly.FullName?.StartsWith(prefix, StringComparison.OrdinalIgnoreCase) == true)
        .AddClassesFromInterfaces());

        return services;
    }

    /// <summary>
    /// Scans and registers services from assemblies whose names contain the specified text.
    /// Services are registered based on their implemented lifetime interfaces (ISingletonLifetime, ITransientLifetime, IScopedLifetime).
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <param name="text">The text that assembly names must contain.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddEasyScrutorForAssembliesContaining(this IServiceCollection services, string text) {
        services.Scan(scan => scan
        .FromDependencyContext(DependencyModel.DependencyContext.Default, assembly => assembly.FullName?.Contains(text, StringComparison.OrdinalIgnoreCase) == true)
        .AddClassesFromInterfaces());

        return services;
    }

    /// <summary>
    /// Configures service registration based on lifetime marker interfaces.
    /// Scans for classes implementing ISingletonLifetime, ITransientLifetime, IScopedLifetime, and their self-registration variants.
    /// </summary>
    /// <param name="selector">The implementation type selector to configure.</param>
    /// <returns>The configured implementation type selector.</returns>
    private static IImplementationTypeSelector AddClassesFromInterfaces(this IImplementationTypeSelector selector) {
        //singleton
        selector
        .AddClasses(classes => classes.AssignableTo<ISingletonLifetime>(), true)
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
}
