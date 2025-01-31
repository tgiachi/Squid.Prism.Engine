using Microsoft.Extensions.DependencyInjection;
using Squid.Prism.Server.Core.Data.Services;

namespace Squid.Prism.Server.Core.Extensions;

public static class RegisterPrismServiceExtension
{
    public static IServiceCollection RegisterPrismService(
        this IServiceCollection services, Type serviceInterface, Type serviceImplementation, int priority = 0
    )
    {
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
}
