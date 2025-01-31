using Microsoft.Extensions.Logging.Testing;
using Moq;
using Squid.Prism.Engine.Core.Data.Events.Variables;
using Squid.Prism.Engine.Core.Impl.Services;
using Squid.Prism.Engine.Core.Interfaces.Listeners;

namespace Squid.Prism.Engine.Tests.Services;

public class EventBusServiceTests
{
    [Test]
    public void EventBusServiceTests_WhenPublishEvent_ThenEventIsPublished()
    {
        // Arrange
        var eventBusService = new EventBusService(new FakeLogger<EventBusService>());
        var eventListener = new Mock<IEventBusListener<AddVariableEvent>>();
        eventBusService.Subscribe(eventListener.Object);

        // Act
        eventBusService.Publish(new AddVariableEvent("test", "test"));

        // Assert
        eventListener.Verify(x => x.OnEventAsync(It.IsAny<AddVariableEvent>()), Times.Once);
    }
}
