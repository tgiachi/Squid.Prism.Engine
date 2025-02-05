using ProtoBuf;

using Squid.Prism.Network.Interfaces.Messages;
using Squid.Prism.Network.Types;

namespace Squid.Prism.Engine.Tests.Data;

[ProtoContract]
public class TestPacket : INetworkMessage
{
    public int RequestType => 0x01;

    [ProtoMember(1)] public string TestKey { get; set; }

    [ProtoMember(2)] public int TestValue { get; set; }
}
