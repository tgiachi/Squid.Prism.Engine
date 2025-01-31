using Squid.Prism.Engine.Core.Interfaces.Events;

namespace Squid.Prism.Engine.Core.Interfaces.Listeners;

public interface IEventBusListener<in TEvent> where TEvent : ISquidPrismEvent
{
    Task OnEventAsync(TEvent message);
}
