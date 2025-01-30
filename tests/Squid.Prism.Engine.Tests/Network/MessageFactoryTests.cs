using Microsoft.Extensions.Logging.Testing;
using Squid.Prism.Engine.Tests.Data;
using Squid.Prism.Network.Data.Configs;
using Squid.Prism.Network.Encoders;
using Squid.Prism.Network.Interfaces.Encoders;
using Squid.Prism.Network.Interfaces.Services;
using Squid.Prism.Network.Services;

namespace Squid.Prism.Engine.Tests.Network;

public class MessageFactoryTests
{
    private readonly INetworkMessageFactory _networkMessageFactory;

    private readonly IMessageTypesService _messageTypesService;

    private readonly INetworkMessageEncoder _networkMessageEncoder;

    private readonly INetworkMessageDecoder _networkMessageDecoder;


    public MessageFactoryTests()
    {
        var settings = new EncoderDecoderSettings();
        _messageTypesService = new MessageTypesService(new FakeLogger<MessageTypesService>());

        _networkMessageEncoder = new ProtobufEncoder(new FakeLogger<ProtobufEncoder>(), settings);
        _networkMessageDecoder = new ProtobufDecoder(new FakeLogger<ProtobufDecoder>(), settings);

        _messageTypesService.RegisterMessage<TestPacket>();
        _messageTypesService.RegisterMessage<TestBigPacket>();

        _networkMessageFactory = new NetworkMessageFactory(
            _messageTypesService,
            _networkMessageDecoder,
            _networkMessageEncoder,
            new FakeLogger<NetworkMessageFactory>()
        );
    }

    [Test]
    public async Task TestSerializeDeserializerAsync()
    {
        var packet = new TestPacket
        {
            TestKey = "TestValue",
            TestValue = 123
        };

        var networkPacket = await _networkMessageFactory.SerializeAsync(packet);

        Assert.That(networkPacket, Is.Not.Null);
        Assert.That(networkPacket.Payload, Is.Not.Null);
        Assert.That(networkPacket.Payload.Length, Is.GreaterThan(0));

        var message = await _networkMessageFactory.ParseAsync(networkPacket);

        Assert.That(message, Is.Not.Null);
        Assert.That(message, Is.TypeOf<TestPacket>());
        Assert.That(((TestPacket)message).TestKey, Is.EqualTo("TestValue"));
        Assert.That(((TestPacket)message).TestValue, Is.EqualTo(123));
    }

    [Test]
    public async Task TestSerializeDeserializeBigPacket()
    {
        var packet = new TestBigPacket()
        {
            Content = (await File.ReadAllBytesAsync(
                Path.Combine(Directory.GetCurrentDirectory(), "MockAssets", "large-file.json")
            )).ToList()
        };

        var networkPacket = await _networkMessageFactory.SerializeAsync(packet);

        Assert.That(networkPacket, Is.Not.Null);
        Assert.That(networkPacket.Payload, Is.Not.Null);
        Assert.That(networkPacket.Payload.Length, Is.GreaterThan(0));

        var message = await _networkMessageFactory.ParseAsync(networkPacket);

        Assert.That(message, Is.Not.Null);
        Assert.That(message, Is.TypeOf<TestBigPacket>());
    }
}
