using Microsoft.Extensions.Logging.Testing;
using Squid.Prism.Engine.Core.Impl.Services;

namespace Squid.Prism.Engine.Tests.Services;

public class VariablesServiceTests
{
    [Test]
    public void GetVariableValue_WhenVariableExists_ShouldReturnVariableValue()
    {
        var eventBusService = new EventBusService(new FakeLogger<EventBusService>());
        var variablesService = new VariablesService(new FakeLogger<VariablesService>(), eventBusService);

        variablesService.AddVariable("test", "mytest");

        var translateValue = variablesService.TranslateText("${test}");

        Assert.That(translateValue, Is.EqualTo("mytest"));


    }
}
