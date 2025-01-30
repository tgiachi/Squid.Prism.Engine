using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Testing;
using Squid.Prism.Engine.Tests.Data;
using Squid.Prism.Network.Data.Configs;
using Squid.Prism.Network.Encoders;
using Squid.Prism.Network.Interfaces.Services;
using Squid.Prism.Network.Services;
using Squid.Prism.Network.Types;

namespace Squid.Prism.Engine.Tests.Network;

public class PacketsTests
{
    private readonly IMessageTypesService _networkMessageMap;

    public PacketsTests()
    {
        _networkMessageMap = new MessageTypesService(NullLogger<MessageTypesService>.Instance);
        _networkMessageMap.RegisterMessage<TestPacket>(0x01);
        _networkMessageMap.RegisterMessage<TestBigPacket>(0x02);
    }

    [Test]
    public async Task TestCreatePacketBrotliAsync()
    {
        var settings = new EncoderDecoderSettings()
        {
            CompressionAlgorithm = CompressionAlgorithmType.Brotli
        };

        var encoder = new ProtobufEncoder(new FakeLogger<ProtobufEncoder>(), settings);
        var decoder = new ProtobufDecoder(new FakeLogger<ProtobufDecoder>(), settings);

        var packet = new TestPacket
        {
            TestKey = "Test",
            TestValue = 1
        };

        var encodedPacket = await encoder.EncodeAsync(packet, packet.MessageType);


        Assert.That(encodedPacket, Is.Not.Null);

        var decodedPacket = await decoder.DecodeAsync(encodedPacket, typeof(TestPacket));

        Assert.That(decodedPacket, Is.Not.Null);
    }

    [Test]
    public async Task TestCreatePacketNoneAsync()
    {
        var settings = new EncoderDecoderSettings()
        {
            CompressionAlgorithm = CompressionAlgorithmType.None
        };

        var encoder = new ProtobufEncoder(new FakeLogger<ProtobufEncoder>(), settings);
        var decoder = new ProtobufDecoder(new FakeLogger<ProtobufDecoder>(), settings);

        var packet = new TestPacket
        {
            TestKey = "Test",
            TestValue = 1
        };

        var encodedPacket = await encoder.EncodeAsync(packet, packet.MessageType);

        Assert.That(encodedPacket, Is.Not.Null);
        var decodedPacket = await decoder.DecodeAsync(encodedPacket, typeof(TestPacket));

        Assert.That(decodedPacket, Is.Not.Null);
    }

    [Test]
    public async Task TestCreatePacketGzipAsync()
    {
        var settings = new EncoderDecoderSettings()
        {
            CompressionAlgorithm = CompressionAlgorithmType.GZip
        };

        var encoder = new ProtobufEncoder(new FakeLogger<ProtobufEncoder>(), settings);
        var decoder = new ProtobufDecoder(new FakeLogger<ProtobufDecoder>(), settings);

        var packet = new TestPacket
        {
            TestKey = "Test",
            TestValue = 1
        };

        var encodedPacket = await encoder.EncodeAsync(packet, packet.MessageType);

        Assert.That(encodedPacket, Is.Not.Null);

        var decodedPacket = await decoder.DecodeAsync(encodedPacket, typeof(TestPacket));

        Assert.That(decodedPacket, Is.Not.Null);
    }

    [Test]
    public async Task TestCreatePacketDeflateAsync()
    {
        var settings = new EncoderDecoderSettings()
        {
            CompressionAlgorithm = CompressionAlgorithmType.Deflate
        };

        var encoder = new ProtobufEncoder(new FakeLogger<ProtobufEncoder>(), settings);
        var decoder = new ProtobufDecoder(new FakeLogger<ProtobufDecoder>(), settings);

        var packet = new TestPacket
        {
            TestKey = "Test",
            TestValue = 1
        };

        var encodedPacket = await encoder.EncodeAsync(packet, packet.MessageType);

        Assert.That(encodedPacket, Is.Not.Null);

        var decodedPacket = await decoder.DecodeAsync(encodedPacket, typeof(TestPacket));

        Assert.That(decodedPacket, Is.Not.Null);
    }

    [Test]
    public async Task TestCreateBigPacketAsync()
    {
        var settings = new EncoderDecoderSettings()
        {
            CompressionAlgorithm = CompressionAlgorithmType.Brotli
        };

        var encoder = new ProtobufEncoder(new FakeLogger<ProtobufEncoder>(), settings);
        var decoder = new ProtobufDecoder(new FakeLogger<ProtobufDecoder>(), settings);


        var packet = new TestBigPacket()
        {
            Content = (await File.ReadAllBytesAsync(
                Path.Combine(Directory.GetCurrentDirectory(), "MockAssets", "large-file.json")
            )).ToList()
        };

        var encodedPacket = await encoder.EncodeAsync(packet, packet.MessageType);

        Assert.That(encodedPacket, Is.Not.Null);

        var decodedPacket = await decoder.DecodeAsync(encodedPacket, typeof(TestBigPacket));

        Assert.That(decodedPacket, Is.Not.Null);
    }
}
