using EasyScrutor;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Scrutor;
using System;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAdvancedDependencyInjection(this IServiceCollection services)
        {
            services.Scan(scan => scan
            .FromDependencyContext(DependencyModel.DependencyContext.Default)
            .AddClassesFromInterfaces());

            return services;
        }

        public static IServiceCollection AddAdvancedDependencyInjection(this IServiceCollection services, Func<Assembly, bool> predicate)
        {
            services.Scan(scan => scan
            .FromDependencyContext(DependencyModel.DependencyContext.Default, predicate)
            .AddClassesFromInterfaces());

            return services;
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
    }
}