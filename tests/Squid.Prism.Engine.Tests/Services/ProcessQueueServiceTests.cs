using Microsoft.Extensions.Logging.Testing;
using Squid.Prism.Engine.Core.Data.Configs;
using Squid.Prism.Engine.Core.Impl.Services;

namespace Squid.Prism.Engine.Tests.Services;

public class ProcessQueueServiceTests
{
    [Test]
    public async Task TestProcessQueueService()
    {
        var processQueueService = new ProcessQueueService(new FakeLogger<ProcessQueueService>(), new ProcessQueueConfig());

        Assert.That(processQueueService, Is.Not.Null);

        processQueueService.Enqueue("test", () => Assert.Pass("Action executed"));

    }
}
