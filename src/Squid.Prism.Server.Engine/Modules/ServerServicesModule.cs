using Microsoft.Extensions.DependencyInjection;
using Squid.Prism.Engine.Core.Interfaces.Modules;
using Squid.Prism.Server.Core.Extensions;
using Squid.Prism.Server.Core.Interfaces.Services;
using Squid.Prism.Server.Engine.Scripts;
using Squid.Prism.Server.Engine.Services;

namespace Squid.Prism.Server.Engine.Modules;

public class ServerServicesModule : IContainerModule
{
    public IServiceCollection RegisterModule(IServiceCollection services)
    {
        return services
            .RegisterPrismService<IEventDispatcherService, EventDispatcherService>()
            .RegisterPrismService<ISchedulerSystemService, SchedulerSystemService>()
            .RegisterPrismService<IHttpServerService, HttpServerService>()
            .RegisterPrismService<IScriptEngineService, ScriptEngineService>()
            .RegisterPrismService<ContextVariableModule>()
            .RegisterPrismService<LoggerModule>()
            .RegisterPrismService<ScriptModule>()
            .RegisterPrismService<EventsModule>()
            .RegisterPrismService<VariableServiceModule>();
    }
}
