using Squid.Prism.Network.Interfaces.Base;

namespace Squid.Prism.Network.Data;

public struct NetworkMessageData
{
    public string SessionId { get; set; }

    public INetworkMessage Message { get; set; }


    public NetworkMessageData(string sessionId, INetworkMessage message)
    {
        SessionId = sessionId;
        Message = message;
    }

    public NetworkMessageData()
    {
    }


    public override string ToString()
    {
        return $"MessageType: {Message.MessageType:B} - SessionId: {SessionId}";
    }
}
