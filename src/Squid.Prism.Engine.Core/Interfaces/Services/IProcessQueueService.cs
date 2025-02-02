using Lumyria.Core.Data.Metrics;
using Squid.Prism.Engine.Core.Interfaces.Services.Base;

namespace Squid.Prism.Engine.Core.Interfaces.Services;

public interface IProcessQueueService : IDisposable, ISquidPrismAutostart
{
    IObservable<ProcessQueueMetric> GetMetrics { get; }
    int MaxParallelTask { get; set; }
    Task<T> Enqueue<T>(string context, Func<T> func, CancellationToken cancellationToken = default);
    Task<T> Enqueue<T>(string context, Func<Task<T>> func, CancellationToken cancellationToken = default);
    Task Enqueue(string context, Action action, CancellationToken cancellationToken = default);
    Task Enqueue(string context, Func<Task> func, CancellationToken cancellationToken = default);
    void EnqueueOnMainThread(Action action);
    void EnqueueOnMainThread<T>(Func<T> func);
    void EnqueueOnMainThread<T>(Func<Task<T>> func);
    void EnqueueOnMainThread(Func<Task> func);
    void ProcessMainThreadQueue();
    void EnsureContext(string context);
}
