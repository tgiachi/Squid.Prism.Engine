using ProtoBuf;
using Squid.Prism.Network.Interfaces.Messages;
using Squid.Prism.Network.Types;

namespace Squid.Prism.Network.Packets;

[ProtoContract]
public class LoginRequestMessage : INetworkMessage
{
    [ProtoMember(1)] public string Username { get; set; }

    [ProtoMember(2)] public string Password { get; set; }

    public int RequestType => DefaultMessageTypeConst.LoginRequest;

    public LoginRequestMessage(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public LoginRequestMessage()
    {
    }
}
