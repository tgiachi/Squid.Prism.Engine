using ProtoBuf;
using Squid.Prism.Network.Interfaces.Messages;
using Squid.Prism.Network.Types;

namespace Squid.Prism.Network.Packets;

[ProtoContract]
public class AssetListRequestMessage : INetworkMessage
{
    public int RequestType => DefaultMessageTypeConst.AssetListRequest;

    public AssetListRequestMessage()
    {
    }


}
