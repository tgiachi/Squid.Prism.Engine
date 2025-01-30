using ProtoBuf;
using Squid.Prism.Network.Interfaces.Base;

namespace Squid.Prism.Engine.Tests.Data;

[ProtoContract]
public class TestBigPacket : INetworkMessage
{
    public int MessageType => 0x02;


    [ProtoMember(1)] public List<byte> Content { get; set; }
}
