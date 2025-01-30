using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Testing;
using Squid.Prism.Network.Interfaces.Messages;
using Squid.Prism.Network.Services;

namespace Squid.Prism.Engine.Tests.Network;

public class MessageTypesServiceTests
{
    private MessageTypesService _service;

    [SetUp]
    public void SetUp()
    {
        _service = new MessageTypesService(new FakeLogger<MessageTypesService>());
    }

    [Test]
    public void RegisterMessageType_ShouldRegisterType()
    {
        _service.RegisterMessageType(1, typeof(MockNetworkMessage));

        Assert.That(typeof(MockNetworkMessage), Is.EqualTo(_service.GetMessageType(1)));
        Assert.That(1, Is.EqualTo(_service.GetMessageType(typeof(MockNetworkMessage))));
    }

    [Test]
    public void RegisterMessageType_ShouldThrowIfTypeNotImplementingINetworkMessage()
    {
        Assert.Throws<ArgumentException>(() => _service.RegisterMessageType(1, typeof(string)));
    }

    [Test]
    public void GetMessageType_ShouldThrowIfMessageTypeNotRegistered()
    {
        Assert.Throws<ArgumentException>(() => _service.GetMessageType(1));
    }

    [Test]
    public void GetMessageTypeByType_ShouldThrowIfTypeNotRegistered()
    {
        Assert.Throws<ArgumentException>(() => _service.GetMessageType(typeof(MockNetworkMessage)));
    }

    [Test]
    public void RegisterMessage_ShouldRegisterGenericType()
    {
        _service.RegisterMessage<MockNetworkMessage>(1);

        Assert.That(typeof(MockNetworkMessage), Is.EqualTo(_service.GetMessageType(1)));
        Assert.That(1, Is.EqualTo(_service.GetMessageType(typeof(MockNetworkMessage))));
    }

    [Test]
    public void RegisterMessageWithoutType_ShouldRegisterGenericType()
    {
        _service.RegisterMessage<MockNetworkMessage>();

        Assert.That(typeof(MockNetworkMessage), Is.EqualTo(_service.GetMessageType(1)));
        Assert.That(1, Is.EqualTo(_service.GetMessageType(typeof(MockNetworkMessage))));
    }

    public class MockNetworkMessage : INetworkMessage
    {
        public int MessageType => 0x01;
    }
}
