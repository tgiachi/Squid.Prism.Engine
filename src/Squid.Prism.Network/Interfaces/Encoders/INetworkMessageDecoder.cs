using Squid.Prism.Network.Interfaces.Messages;
using Squid.Prism.Network.Interfaces.Packets;

namespace Squid.Prism.Network.Interfaces.Encoders;

public interface INetworkMessageDecoder
{
    Task<INetworkMessage> DecodeAsync(INetworkPacket packet, Type type);
}
