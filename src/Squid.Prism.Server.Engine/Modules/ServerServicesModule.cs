using Microsoft.Extensions.DependencyInjection;
using Squid.Prism.Engine.Core.Interfaces.Modules;
using Squid.Prism.Network.Packets;
using Squid.Prism.Network.Server.Extensions;
using Squid.Prism.Network.Types;
using Squid.Prism.Server.Core.Entities;
using Squid.Prism.Server.Core.Extensions;
using Squid.Prism.Server.Core.Interfaces.Services;
using Squid.Prism.Server.Core.Interfaces.Services.Game;
using Squid.Prism.Server.Engine.Services;
using Squid.Prism.Server.Engine.Services.Game;
using Squid.Prism.Server.Engine.Services.Handlers;

namespace Squid.Prism.Server.Engine.Modules;

public class ServerServicesModule : IContainerModule
{
    public IServiceCollection RegisterModule(IServiceCollection services)
    {
        var s = services
            .RegisterPrismService<IEventDispatcherService, EventDispatcherService>()
            .RegisterPrismService<ISchedulerSystemService, SchedulerSystemService>()
            .RegisterPrismService<IHttpServerService, HttpServerService>()
            .RegisterPrismService<IDiagnosticSystemService, DiagnosticSystemService>()
            .RegisterPrismService<IScriptEngineService, ScriptEngineService>(10);


        services
            .RegisterPrismService<IPlayerService, PlayerService>()
            .RegisterPrismService<IWorldService, WorldService>()
            .RegisterPrismService<IBlockService, BlockService>()
            .RegisterPrismService<IAssetService, AssetService>()
            ;


        services
            .RegisterPrismService<AssetHandler>()
            ;


        services
            .RegisterPrismService<IDatabaseService, LiteDbDatabaseService>(9)
            .RegisterDatabaseEntity<UserEntity>();


        services
            .RegisterNetworkMessage<VersionRequestMessage>(DefaultMessageTypeConst.VersionRequest)
            .RegisterNetworkMessage<VersionResponseMessage>(DefaultMessageTypeConst.VersionResponse)
            .RegisterNetworkMessage<MotdRequestMessage>(DefaultMessageTypeConst.MotdRequest)
            .RegisterNetworkMessage<MotdResponseMessage>(DefaultMessageTypeConst.MotdResponse)
            .RegisterNetworkMessage<LoginRequestMessage>(DefaultMessageTypeConst.LoginRequest)
            .RegisterNetworkMessage<LoginResponseMessage>(DefaultMessageTypeConst.LoginResponse)
            .RegisterNetworkMessage<AssetListRequestMessage>(DefaultMessageTypeConst.AssetListRequest)
            .RegisterNetworkMessage<AssetListResponseMessage>(DefaultMessageTypeConst.AssetListResponse)
            .RegisterNetworkMessage<AssetRequestMessage>(DefaultMessageTypeConst.AssetRequest)
            .RegisterNetworkMessage<AssetResponseMessage>(DefaultMessageTypeConst.AssetResponse)
            .RegisterNetworkMessage<PlayerMoveRequestMessage>(DefaultMessageTypeConst.PlayerMoveRequest)
            .RegisterNetworkMessage<PlayerMoveResponseMessage>(DefaultMessageTypeConst.PlayerMoveResponse)
            ;


        return s;
    }
}
