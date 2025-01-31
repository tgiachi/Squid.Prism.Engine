using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Squid.Prism.Server.Core.Attributes.Scripts;
using Squid.Prism.Server.Core.Data.Scripts;
using Squid.Prism.Server.Core.Data.Services;

namespace Squid.Prism.Server.Core.Extensions;

public static class RegisterPrismServiceExtension
{
    public static IServiceCollection RegisterPrismService(
        this IServiceCollection services, Type serviceInterface, Type serviceImplementation, int priority = 0
    )
    {
        SearchForScriptAttribute(serviceImplementation, services);

        return services.AddSingleton(serviceInterface, serviceImplementation)
            .AddToRegisterTypedList(new ServiceDefinitionData(serviceImplementation, serviceInterface, priority));
    }

    public static IServiceCollection RegisterPrismService<TServiceInterface, TServiceImplementation>(
        this IServiceCollection services, int priority = 0
    )
        where TServiceInterface : class
        where TServiceImplementation : class, TServiceInterface
    {
        return services.RegisterPrismService(typeof(TServiceInterface), typeof(TServiceImplementation), priority);
    }

    public static IServiceCollection RegisterPrismService<TService>(this IServiceCollection services, int priority = 0)
    {
        return services.RegisterPrismService(typeof(TService), typeof(TService), priority);
    }

    public static IServiceCollection RegisterPrismService(
        this IServiceCollection services, Type serviceType, int priority = 0
    )
    {
        return services.RegisterPrismService(serviceType, serviceType, priority);
    }

    private static void SearchForScriptAttribute(Type serviceType, IServiceCollection services)
    {
        if (serviceType.GetCustomAttribute<ScriptModuleAttribute>() != null)
        {
            services.AddToRegisterTypedList(new ScriptClassData(serviceType));
        }
    }
}
