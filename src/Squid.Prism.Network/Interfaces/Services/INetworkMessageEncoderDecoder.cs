using Squid.Prism.Network.Interfaces.Base;

namespace Squid.Prism.Network.Interfaces.Services;

public interface INetworkMessageEncoderDecoder
{
    Task<INetworkPacket> EncodeAsync(INetworkMessage message);

    Task<INetworkMessage> DecodeAsync(INetworkPacket packet);
}
