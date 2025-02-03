using Microsoft.Extensions.DependencyInjection;
using Squid.Prism.Engine.Core.Interfaces.Modules;
using Squid.Prism.Network.Packets;
using Squid.Prism.Network.Server.Extensions;
using Squid.Prism.Network.Types;
using Squid.Prism.Server.Core.Entities;
using Squid.Prism.Server.Core.Extensions;
using Squid.Prism.Server.Core.Interfaces.Services;
using Squid.Prism.Server.Core.Interfaces.Services.Game;
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
            .RegisterPrismService<IScriptEngineService, ScriptEngineService>(10);


        s
            .RegisterPrismService<IPlayerService, PlayerService>()
            .RegisterPrismService<IWorldService, WorldService>()
            .RegisterPrismService<IBlockService, BlockService>()
            ;


        s
            .RegisterPrismService<IDatabaseService, LiteDbDatabaseService>(9)
            .RegisterDatabaseEntity<UserEntity>();


        s
            .RegisterNetworkMessage<VersionRequestMessage>(DefaultMessageTypeConst.VersionMessageRequest)
            .RegisterNetworkMessage<VersionResponseMessage>(DefaultMessageTypeConst.VersionMessageResponse)
            .RegisterNetworkMessage<MotdRequestMessage>(DefaultMessageTypeConst.MotdMessageRequest)
            .RegisterNetworkMessage<MotdResponseMessage>(DefaultMessageTypeConst.MotdMessageResponse)
            .RegisterNetworkMessage<LoginRequestMessage>(DefaultMessageTypeConst.LoginMessageRequest)
            .RegisterNetworkMessage<LoginResponseMessage>(DefaultMessageTypeConst.LoginMessageResponse)
            ;


        return s;
    }
}
