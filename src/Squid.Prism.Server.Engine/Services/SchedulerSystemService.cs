using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reactive.Linq;
using Microsoft.Extensions.Logging;
using Squid.Prism.Engine.Core.Interfaces.Services;
using Squid.Prism.Server.Core.Events.Scheduler;
using Squid.Prism.Server.Core.Interfaces.Services;

using Squid.Prism.Server.Engine.Data.Scheduler;

namespace Squid.Prism.Server.Engine.Services;

public class SchedulerSystemService : ISchedulerSystemService
{
    private readonly ILogger _logger;
    private readonly ConcurrentDictionary<string, ScheduledJob> _jobs;
    private readonly ConcurrentDictionary<string, IDisposable> _pausedJobs;

    public SchedulerSystemService(IEventBusService eventBusService, ILogger<SchedulerSystemService> logger)
    {
        _logger = logger;
        _jobs = new ConcurrentDictionary<string, ScheduledJob>();
        _pausedJobs = new ConcurrentDictionary<string, IDisposable>();
        eventBusService.Subscribe<AddSchedulerJobEvent>(OnAddSchedulerJob);
    }

    private void OnAddSchedulerJob(AddSchedulerJobEvent obj)
    {
        _logger.LogInformation("Registering job '{JobName}'", obj.Name);
        _ = RegisterJob(obj.Name, obj.Action, obj.TotalSpan);
    }

    public async Task RegisterJob(string name, Func<Task> task, TimeSpan interval)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("Job name cannot be empty", nameof(name));
        }

        ArgumentNullException.ThrowIfNull(task);

        if (interval <= TimeSpan.Zero)
        {
            throw new ArgumentException("Interval must be positive", nameof(interval));
        }

        if (await IsJobRegistered(name))
        {
            throw new InvalidOperationException($"Job '{name}' is already registered");
        }

        var subscription = Observable
            .Interval(interval)
            .Subscribe(
                async _ =>
                {
                    try
                    {
                        await ExecuteJob(_jobs[name]);
                    }
                    catch (Exception ex)
                    {
                        // Log the exception or handle it according to your needs
                        _logger.LogError(ex, "Error occurred while executing job '{JobName}'", name);
                    }
                }
            );

        var job = new ScheduledJob
        {
            Name = name,
            Interval = interval,
            Task = task,
            Subscription = subscription
        };

        _jobs.TryAdd(name, job);
    }

    public async Task UnregisterJob(string name)
    {
        if (await IsJobRegistered(name))
        {
            if (_jobs.TryRemove(name, out var job))
            {
                job.Subscription?.Dispose();
            }
        }
    }

    public Task<bool> IsJobRegistered(string name)
    {
        return Task.FromResult(_jobs.ContainsKey(name));
    }

    public async Task PauseJob(string name)
    {
        if (!await IsJobRegistered(name))
        {
            throw new InvalidOperationException($"Job '{name}' is not registered");
        }

        if (_jobs.TryGetValue(name, out var job))
        {
            job.Subscription?.Dispose();
            _pausedJobs.TryAdd(name, job.Subscription);
        }
    }

    private async Task ExecuteJob(ScheduledJob job)
    {
        var startTime = Stopwatch.GetTimestamp();
        _logger.LogInformation("Executing job '{JobName}'", job.Name);
        await job.Task();
        var elapsed = Stopwatch.GetElapsedTime(startTime);

        _logger.LogInformation("Job '{JobName}' executed in {Elapsed} ms", job.Name, elapsed);
    }

    public async Task ResumeJob(string name)
    {
        if (!await IsJobRegistered(name))
        {
            throw new InvalidOperationException($"Job '{name}' is not registered");
        }

        if (_jobs.TryGetValue(name, out var job))
        {
            var subscription = Observable
                .Interval(job.Interval)
                .Subscribe(async _ => await ExecuteJob(_jobs[name]));

            job.Subscription = subscription;
            _pausedJobs.TryRemove(name, out _);
        }
    }

    public void Dispose()
    {
        foreach (var job in _jobs.Values)
        {
            job.Subscription?.Dispose();
        }

        _jobs.Clear();
        _pausedJobs.Clear();
        GC.SuppressFinalize(this);
    }

    public Task StartAsync(CancellationToken cancellationToken=default)
    {
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken=default)
    {
        return Task.CompletedTask;
    }
}
