using ProtoBuf;
using Squid.Prism.Network.Interfaces.Messages;
using Squid.Prism.Network.Types;

namespace Squid.Prism.Network.Packets;

[ProtoContract]
public class LoginResponseMessage : INetworkMessage
{
    public int RequestType => DefaultMessageTypeConst.LoginResponse;

    [ProtoMember(1)] public bool Success { get; set; }

    [ProtoMember(2)] public string Message { get; set; }

    public LoginResponseMessage(bool success, string message)
    {
        Success = success;
        Message = message;
    }

    public LoginResponseMessage()
    {
    }
}
