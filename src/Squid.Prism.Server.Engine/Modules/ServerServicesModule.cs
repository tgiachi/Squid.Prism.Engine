using Microsoft.Extensions.DependencyInjection;
using Squid.Prism.Engine.Core.Interfaces.Modules;
using Squid.Prism.Network.Packets;
using Squid.Prism.Network.Server.Extensions;
using Squid.Prism.Network.Types;
using Squid.Prism.Server.Core.Extensions;
using Squid.Prism.Server.Core.Interfaces.Services;
using Squid.Prism.Server.Engine.Scripts;
using Squid.Prism.Server.Engine.Services;
using Squid.Prism.Server.Engine.Services.Game;

namespace Squid.Prism.Server.Engine.Modules;

public class ServerServicesModule : IContainerModule
{
    public IServiceCollection RegisterModule(IServiceCollection services)
    {
        var s = services
            .RegisterPrismService<IEventDispatcherService, EventDispatcherService>()
            .RegisterPrismService<ISchedulerSystemService, SchedulerSystemService>()
            .RegisterPrismService<IHttpServerService, HttpServerService>()
            .RegisterPrismService<IScriptEngineService, ScriptEngineService>()
            .RegisterPrismService<ContextVariableModule>()
            .RegisterPrismService<LoggerModule>()
            .RegisterPrismService<ScriptModule>()
            .RegisterPrismService<EventsModule>()
            .RegisterPrismService<FileModule>()
            .RegisterPrismService<PlayerServiceModule>()
            .RegisterPrismService<VariableServiceModule>();


        s.RegisterPrismService<IPlayerService, PlayerService>()
            ;


        s
            .RegisterNetworkMessage<VersionRequestMessage>(DefaultMessageTypeConst.VersionMessageRequest)
            .RegisterNetworkMessage<VersionResponseMessage>(DefaultMessageTypeConst.VersionMessageResponse);


        return s;
    }
}
