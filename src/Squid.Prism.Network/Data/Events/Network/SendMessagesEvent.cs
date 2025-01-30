using Squid.Prism.Network.Interfaces.Messages;

namespace Squid.Prism.Network.Data.Events.Network;

public class SendMessagesEvent
{
    public string SessionId { get; set; }

    public INetworkMessage[] Messages { get; set; }

    public SendMessagesEvent(string sessionId, params INetworkMessage[] messages)
    {
        SessionId = sessionId;
        Messages = messages;
    }

    public override string ToString()
    {
        return $"SessionId: {(SessionId == string.Empty ? "Broadcast" : SessionId)}, Message: {Messages.Length}";
    }
}
