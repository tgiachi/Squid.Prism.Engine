using Squid.Prism.Engine.Core.Interfaces.Events;

namespace Squid.Prism.Engine.Core.Data.Events.Variables;

public record AddVariableBuilderEvent(string VariableName, Func<object> Builder) : ISquidPrismEvent;
