using Squid.Prism.Engine.Core.Interfaces.Services.Base;

namespace Squid.Prism.Server.Core.Interfaces.Services;

public interface ISchedulerSystemService : IDisposable, ISquidPrismAutostart
{
    Task RegisterJob(string name, Func<Task> task, TimeSpan interval);
    Task UnregisterJob(string name);
    Task<bool> IsJobRegistered(string name);
    Task PauseJob(string name);
    Task ResumeJob(string name);
}
