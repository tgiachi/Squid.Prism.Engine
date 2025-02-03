using Squid.Prism.Engine.Core.Interfaces.Services.Base;

namespace Squid.Prism.Server.Core.Interfaces.Services;

public interface IEventDispatcherService : ISquidPrismService
{
    void SubscribeToEvent(string eventName, Action<object?> eventHandler);

    void UnsubscribeFromEvent(string eventName, Action<object?> eventHandler);
}
