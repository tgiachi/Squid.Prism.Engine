using ProtoBuf;
using Squid.Prism.Network.Interfaces.Messages;
using Squid.Prism.Network.Types;

namespace Squid.Prism.Network.Packets;

[ProtoContract]
public class AssetListResponseMessage : INetworkMessage
{
    [ProtoMember(1)]
    public List<(string name, byte assetType)> Assets { get; set; } = new();

    public int RequestType => DefaultMessageTypeConst.AssetListResponse;

    public AssetListResponseMessage()
    {
    }

    public AssetListResponseMessage(List<(string name, byte assetType)> assets)
    {
        Assets = assets;
    }
}
