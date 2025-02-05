using ProtoBuf;
using Squid.Prism.Network.Interfaces.Messages;
using Squid.Prism.Network.Types;

namespace Squid.Prism.Network.Packets;

[ProtoContract]
public class AssetResponseMessage : INetworkMessage
{
    public int RequestType => DefaultMessageTypeConst.AssetResponse;

    [ProtoMember(1)] public string Name { get; set; }

    [ProtoMember(2)] public List<byte> Data { get; set; }

    [ProtoMember(3)] public long Size { get; set; }

    [ProtoMember(4)] public int CurrentPart { get; set; }

    [ProtoMember(5)] public int TotalParts { get; set; }

    public AssetResponseMessage()
    {
    }

    public AssetResponseMessage(string name, byte[] data, long size, int currentPart, int totalParts)
    {
        Name = name;
        Data = data.ToList();
        Size = size;
        CurrentPart = currentPart;
        TotalParts = totalParts;
    }
}
