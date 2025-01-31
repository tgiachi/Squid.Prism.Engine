using Squid.Prism.Engine.Core.Interfaces.Configs;

namespace Squid.Prism.Network.Server.Configs;

public class NetworkServerConfig : ISquidPrismConfig
{
    public int Port { get; set; } = 6669;
}
