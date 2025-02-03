using Microsoft.Extensions.DependencyInjection;
using Squid.Prism.Engine.Core.Interfaces.Modules;
using Squid.Prism.Network.Encoders;
using Squid.Prism.Network.Interfaces.Encoders;
using Squid.Prism.Network.Interfaces.Metrics.Server;
using Squid.Prism.Network.Interfaces.Services;
using Squid.Prism.Network.Services;
using Squid.Prism.Server.Core.Extensions;

namespace Squid.Prism.Network.Server.Modules;

public class NetworkServiceModule : IContainerModule
{
    public IServiceCollection RegisterModule(IServiceCollection services)
    {
        services
            .RegisterPrismService<INetworkServer, NetworkServer>(10)
            .AddSingleton<IMessageDispatcherService, MessageDispatcherService>()
            .AddSingleton<IMessageParserWriterService, MessageParserWriterService>()
            .AddSingleton<INetworkMessageFactory, NetworkMessageFactory>()
            .AddSingleton<INetworkSessionService, NetworkSessionService>()
            .AddSingleton<NetworkServerMetrics>()
            .AddSingleton<IMessageChannelService, MessageChannelService>()
            .AddSingleton<IMessageTypesService, MessageTypesService>()
            .AddSingleton<INetworkMessageEncoder, ProtobufEncoder>()
            .AddSingleton<INetworkMessageDecoder, ProtobufDecoder>()
            ;


        return services;
    }
}
