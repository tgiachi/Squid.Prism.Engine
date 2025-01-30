
namespace Squid.Prism.Network.Data.Events.Sessions;

public class SessionRemovedEvent
{
    public string SessionId { get; }

    public SessionRemovedEvent(string sessionId)
    {
        SessionId = sessionId;
    }
}
