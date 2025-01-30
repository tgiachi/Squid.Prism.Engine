using Squid.Prism.Network.Types;

namespace Squid.Prism.Network.Interfaces.Providers;

public interface INetworkChannelProvider
{
    NetworkChannelType ChannelType { get; }

    bool IsConnected { get; }


}
