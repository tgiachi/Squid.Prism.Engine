namespace Squid.Prism.Server.Engine.Data.Scheduler;

public class ScheduledJob
{
    public string Name { get; set; }
    public TimeSpan Interval { get; set; }
    public Func<Task> Task { get; set; }
    public IDisposable Subscription { get; set; }
}
