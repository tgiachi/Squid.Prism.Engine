using Squid.Prism.Engine.Core.Interfaces.Configs;

namespace Squid.Prism.Engine.Core.Data.Configs;

public class ProcessQueueConfig : ISquidPrismConfig
{
    public int MaxParallelTasks { get; set; } = 2;
}
