using ProtoBuf;
using Squid.Prism.Network.Interfaces.Messages;
using Squid.Prism.Network.Types;

namespace Squid.Prism.Network.Packets;

[ProtoContract]
public class AssetRequestMessage : INetworkMessage
{
    public int RequestType => DefaultMessageTypeConst.AssetRequest;

    [ProtoMember(1)] public string Name { get; set; }

    public AssetRequestMessage()
    {
    }

    public AssetRequestMessage(string name)
    {
        Name = name;
    }
}
