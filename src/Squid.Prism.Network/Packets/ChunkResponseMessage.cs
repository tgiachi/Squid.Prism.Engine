using Squid.Prism.Network.Interfaces.Messages;
using Squid.Prism.Network.Types;

namespace Squid.Prism.Network.Packets;

public class ChunkResponseMessage : INetworkMessage
{
    public int RequestType => DefaultMessageTypeConst.ChunkResponse;
}
