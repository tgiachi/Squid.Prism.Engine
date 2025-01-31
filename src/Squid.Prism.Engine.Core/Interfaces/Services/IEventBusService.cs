using System.Reactive.Subjects;
using Squid.Prism.Engine.Core.Interfaces.Events;
using Squid.Prism.Engine.Core.Interfaces.Listeners;

namespace Squid.Prism.Engine.Core.Interfaces.Services;

public interface IEventBusService
{
    Task PublishAsync<TEvent>(TEvent eventItem) where TEvent : class, ISquidPrismEvent;
    void Publish<TEvent>(TEvent eventItem) where TEvent : class, ISquidPrismEvent;
    IDisposable Subscribe<TEvent>(Action<TEvent> handler) where TEvent : class, ISquidPrismEvent;
    void Subscribe<TEvent>(IEventBusListener<TEvent> listener) where TEvent : class, ISquidPrismEvent;
    IDisposable SubscribeAsync<TEvent>(Func<TEvent, Task> handler) where TEvent : class, ISquidPrismEvent;
    ISubject<object> AllEventsObservable { get; }
}
