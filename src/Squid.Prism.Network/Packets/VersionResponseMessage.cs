using ProtoBuf;
using Squid.Prism.Network.Interfaces.Messages;
using Squid.Prism.Network.Types;

namespace Squid.Prism.Network.Packets;

[ProtoContract]
public class VersionResponseMessage : INetworkMessage
{
    public int RequestType => DefaultMessageTypeConst.VersionResponse;

    [ProtoMember(1)] public string Version { get; set; }

    public VersionResponseMessage()
    {
        Version = string.Empty;
    }

    public VersionResponseMessage(string version)
    {
        Version = version;
    }
}
