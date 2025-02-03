using NLua;
using Squid.Prism.Server.Core.Attributes.Scripts;
using Squid.Prism.Server.Core.Interfaces.Services;

namespace Squid.Prism.Server.Engine.Scripts;

[ScriptModule("scheduler")]
public class SchedulerJobModule
{
    private readonly ISchedulerSystemService _schedulerSystemService;

    public SchedulerJobModule(ISchedulerSystemService schedulerSystemService)
    {
        _schedulerSystemService = schedulerSystemService;
    }

    [ScriptFunction("add_job", "Add a job to the scheduler, Interval in seconds")]
    public void AddJob(string name, LuaFunction callback, int intervalInSeconds)
    {
        _schedulerSystemService.RegisterJob(name, async () => { callback.Call(); }, TimeSpan.FromSeconds(intervalInSeconds));
    }
}
