using Squid.Prism.Engine.Core.Interfaces.Events;

namespace Squid.Prism.Server.Core.Events.Scheduler;

public record AddSchedulerJobEvent(string Name, TimeSpan TotalSpan, Func<Task> Action) : ISquidPrismEvent;
