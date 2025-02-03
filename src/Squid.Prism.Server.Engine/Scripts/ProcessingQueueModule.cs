using NLua;
using Squid.Prism.Engine.Core.Interfaces.Services;
using Squid.Prism.Server.Core.Attributes.Scripts;

namespace Squid.Prism.Server.Engine.Scripts;

[ScriptModule("task_queue")]
public class ProcessingQueueModule
{
    private readonly IProcessQueueService _processQueueService;

    public ProcessingQueueModule(IProcessQueueService processQueueService)
    {
        _processQueueService = processQueueService;
    }

    [ScriptFunction("add_task")]
    public void AddTask(LuaFunction callback)
    {
        _processQueueService.Enqueue("lua", async () => { callback.Call(); });
    }

    [ScriptFunction("add_task_main_thread")]
    public void AddTaskToMainThread(LuaFunction callback)
    {
        _processQueueService.EnqueueOnMainThread(async () => { callback.Call(); });
    }
}
