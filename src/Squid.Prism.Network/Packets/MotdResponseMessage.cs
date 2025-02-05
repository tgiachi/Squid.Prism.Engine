using ProtoBuf;
using Squid.Prism.Network.Interfaces.Messages;
using Squid.Prism.Network.Types;

namespace Squid.Prism.Network.Packets;

[ProtoContract]
public class MotdResponseMessage : INetworkMessage
{
    public int RequestType => DefaultMessageTypeConst.MotdResponse;

    [ProtoMember(1)] public List<string> Message { get; set; }

    public MotdResponseMessage(string[] message)
    {
        Message = message.ToList();
    }

    public MotdResponseMessage()
    {
    }
}
