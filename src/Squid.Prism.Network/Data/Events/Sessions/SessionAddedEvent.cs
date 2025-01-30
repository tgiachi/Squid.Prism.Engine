namespace Squid.Prism.Network.Data.Events.Sessions;

public class SessionAddedEvent
{
    public string SessionId { get; }

    public SessionAddedEvent(string sessionId)
    {
        SessionId = sessionId;
    }

    public override string ToString()
    {
        return $"SessionAddedEvent: {SessionId}";
    }


}
