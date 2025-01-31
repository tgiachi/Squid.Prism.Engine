using Squid.Prism.Engine.Core.Interfaces.Events;

namespace Squid.Prism.Network.Data.Events.Clients;

public class ClientDisconnectedEvent(string SessionId) : ISquidPrismEvent;
