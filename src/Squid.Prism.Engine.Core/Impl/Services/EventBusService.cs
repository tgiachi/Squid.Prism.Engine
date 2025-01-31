using System.Collections.Concurrent;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Microsoft.Extensions.Logging;
using Squid.Prism.Engine.Core.Interfaces.Events;
using Squid.Prism.Engine.Core.Interfaces.Listeners;
using Squid.Prism.Engine.Core.Interfaces.Services;

namespace Squid.Prism.Engine.Core.Impl.Services;

public class EventBusService : IEventBusService
{
    private readonly ILogger _logger;

    private readonly ConcurrentDictionary<Type, object> _subjects = new();

    public EventBusService(ILogger<EventBusService> logger)
    {
        _logger = logger;
    }

    public ISubject<object> AllEventsObservable { get; } = new Subject<object>();


    public async Task PublishAsync<TEvent>(TEvent eventItem) where TEvent : class, ISquidPrismEvent
    {
        _logger.LogDebug("Publishing event: {Event}", eventItem);
        if (_subjects.TryGetValue(typeof(TEvent), out var subject))
        {
            var typedSubject = (ISubject<TEvent>)subject;
            typedSubject.OnNext(eventItem);
        }

        AllEventsObservable.OnNext(eventItem);
    }

    public void Publish<TEvent>(TEvent eventItem) where TEvent : class, ISquidPrismEvent
    {
        _logger.LogDebug("Publishing event: {Event}", eventItem);

        if (_subjects.TryGetValue(typeof(TEvent), out var subject))
        {
            var typedSubject = (ISubject<TEvent>)subject;
            typedSubject.OnNext(eventItem);
        }

        AllEventsObservable.OnNext(eventItem);
    }

    public IDisposable Subscribe<TEvent>(Action<TEvent> handler) where TEvent : class, ISquidPrismEvent
    {
        var subject = (ISubject<TEvent>)_subjects.GetOrAdd(typeof(TEvent), _ => new Subject<TEvent>());
        return subject.AsObservable().Subscribe(handler);
    }

    public IDisposable SubscribeAsync<TEvent>(Func<TEvent, Task> handler) where TEvent : class, ISquidPrismEvent
    {
        var subject = (ISubject<TEvent>)_subjects.GetOrAdd(typeof(TEvent), _ => new Subject<TEvent>());
        return subject.AsObservable().Subscribe(async e => await handler(e));
    }


    public void Subscribe<TEvent>(IEventBusListener<TEvent> listener) where TEvent : class, ISquidPrismEvent
    {
        var subject = (ISubject<TEvent>)_subjects.GetOrAdd(typeof(TEvent), _ => new Subject<TEvent>());
        subject.AsObservable().Subscribe(async e => await listener.OnEventAsync(e));
    }
}
