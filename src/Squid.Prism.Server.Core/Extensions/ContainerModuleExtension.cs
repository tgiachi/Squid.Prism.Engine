using Microsoft.Extensions.DependencyInjection;
using Squid.Prism.Engine.Core.Interfaces.Modules;

namespace Squid.Prism.Server.Core.Extensions;

public static class ContainerModuleExtension
{
    public static IServiceCollection LoadContainerModule(this IServiceCollection services, Type containerModule)
    {
        var module = Activator.CreateInstance(containerModule) as IContainerModule;

        if (module == null)
        {
            throw new Exception($"Container module {containerModule.Name} is not a valid container module");
        }

        module.RegisterModule(services);

        return services;
    }

    public static IServiceCollection LoadContainerModules(
        this IServiceCollection services, IEnumerable<Type> containerModules
    )
    {
        foreach (var containerModule in containerModules)
        {
            services.LoadContainerModule(containerModule);
        }

        return services;
    }

    public static IServiceCollection LoadContainerModule<TModule>(this IServiceCollection services)
        where TModule : IContainerModule
    {
        return services.LoadContainerModule(typeof(TModule));
    }
}
