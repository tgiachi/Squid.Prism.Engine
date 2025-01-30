using Squid.Prism.Network.Interfaces.Packets;
using Squid.Prism.Network.Types;

namespace Squid.Prism.Network.Interfaces.Encoders;

public interface INetworkMessageEncoder
{
    Task<INetworkPacket> EncodeAsync<TMessage>(TMessage message, int messageType) where TMessage : class;
}
