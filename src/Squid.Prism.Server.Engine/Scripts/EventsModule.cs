using NLua;
using Squid.Prism.Server.Core.Attributes.Scripts;
using Squid.Prism.Server.Core.Interfaces.Services;

namespace Squid.Prism.Server.Engine.Scripts;

[ScriptModule("events")]
public class EventsModule
{
    private readonly IEventDispatcherService _eventDispatcherService;

    public EventsModule(IEventDispatcherService eventDispatcherService)
    {
        _eventDispatcherService = eventDispatcherService;
    }


    [ScriptFunction("on_event")]
    public void AddEvent(string eventName, LuaFunction function)
    {
        _eventDispatcherService.SubscribeToEvent(eventName, (obj) => { function.Call(obj); });
    }
}
