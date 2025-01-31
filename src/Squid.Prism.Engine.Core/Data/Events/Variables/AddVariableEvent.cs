using Squid.Prism.Engine.Core.Interfaces.Events;

namespace Squid.Prism.Engine.Core.Data.Events.Variables;

public record AddVariableEvent(string VariableName, object Value) : ISquidPrismEvent;
