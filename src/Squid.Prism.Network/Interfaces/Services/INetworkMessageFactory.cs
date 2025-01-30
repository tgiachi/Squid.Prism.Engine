using Squid.Prism.Network.Interfaces.Messages;
using Squid.Prism.Network.Interfaces.Packets;

namespace Squid.Prism.Network.Interfaces.Services;

public interface INetworkMessageFactory
{
    Task<INetworkPacket> SerializeAsync<T>(T message) where T : class;

    Task<INetworkMessage> ParseAsync(INetworkPacket packet);
}
