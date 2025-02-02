using Microsoft.Extensions.DependencyInjection;
using Squid.Prism.Engine.Core.Impl.Services;
using Squid.Prism.Engine.Core.Interfaces.Modules;
using Squid.Prism.Engine.Core.Interfaces.Services;
using Squid.Prism.Server.Core.Extensions;
using Squid.Prism.Server.Core.Interfaces.Services;

namespace Squid.Prism.Server.Core.Modules;

public class CoreServiceModule : IContainerModule
{
    public IServiceCollection RegisterModule(IServiceCollection services)
    {
        return services
                .RegisterPrismService<IEventBusService, EventBusService>()
                .RegisterPrismService<IVariablesService, VariablesService>(1)
                .RegisterPrismService<IVersionService, VersionService>()
            ;
    }
}
