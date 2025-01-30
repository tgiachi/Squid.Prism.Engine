using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Testing;
using Squid.Prism.Engine.Tests.Data;
using Squid.Prism.Network.Impl.Encoders;
using Squid.Prism.Network.Interfaces.Services;
using Squid.Prism.Network.Types;

namespace Squid.Prism.Engine.Tests.Network;

public class PacketsTests
{
    private readonly INetworkMessageMapService _networkMessageMap;

    public PacketsTests()
    {
        _networkMessageMap = new NetworkMessageMapService(NullLogger<NetworkMessageMapService>.Instance);
        _networkMessageMap.RegisterMessage<TestPacket>(0x01);
        _networkMessageMap.RegisterMessage<TestBigPacket>(0x02);
    }

    [Test]
    public async Task TestCreatePacketBrotliAsync()
    {
        var encoderDecoder = new DefaultMessageEncoderDecoder(
            new FakeLogger<DefaultMessageEncoderDecoder>(),
            _networkMessageMap,
            CompressionAlgorithmType.Brotli
        );

        var packet = new TestPacket
        {
            TestKey = "Test",
            TestValue = 1
        };

        var encodedPacket = await encoderDecoder.EncodeAsync(packet);


        Assert.That(encodedPacket, Is.Not.Null);

        var decodedPacket = await encoderDecoder.DecodeAsync(encodedPacket);

        Assert.That(decodedPacket, Is.Not.Null);
    }

    [Test]
    public async Task TestCreatePacketNoneAsync()
    {
        var encoderDecoder = new DefaultMessageEncoderDecoder(
            new FakeLogger<DefaultMessageEncoderDecoder>(),
            _networkMessageMap,
            CompressionAlgorithmType.None
        );

        var packet = new TestPacket
        {
            TestKey = "Test",
            TestValue = 1
        };

        var encodedPacket = await encoderDecoder.EncodeAsync(packet);

        Assert.That(encodedPacket, Is.Not.Null);
        var decodedPacket = await encoderDecoder.DecodeAsync(encodedPacket);

        Assert.That(decodedPacket, Is.Not.Null);
    }

    [Test]
    public async Task TestCreatePacketGzipAsync()
    {
        var encoderDecoder = new DefaultMessageEncoderDecoder(
            new FakeLogger<DefaultMessageEncoderDecoder>(),
            _networkMessageMap,
            CompressionAlgorithmType.GZip
        );

        var packet = new TestPacket
        {
            TestKey = "Test",
            TestValue = 1
        };

        var encodedPacket = await encoderDecoder.EncodeAsync(packet);

        Assert.That(encodedPacket, Is.Not.Null);

        var decodedPacket = await encoderDecoder.DecodeAsync(encodedPacket);

        Assert.That(decodedPacket, Is.Not.Null);
    }

    [Test]
    public async Task TestCreatePacketDeflateAsync()
    {
        var encoderDecoder = new DefaultMessageEncoderDecoder(
            new FakeLogger<DefaultMessageEncoderDecoder>(),
            _networkMessageMap,
            CompressionAlgorithmType.Deflate
        );

        var packet = new TestPacket
        {
            TestKey = "Test",
            TestValue = 1
        };

        var encodedPacket = await encoderDecoder.EncodeAsync(packet);

        Assert.That(encodedPacket, Is.Not.Null);

        var decodedPacket = await encoderDecoder.DecodeAsync(encodedPacket);

        Assert.That(decodedPacket, Is.Not.Null);
    }

    [Test]
    public async Task TestCreateBigPacketAsync()
    {
        var encoderDecoder = new DefaultMessageEncoderDecoder(
            new FakeLogger<DefaultMessageEncoderDecoder>(),
            _networkMessageMap,
            CompressionAlgorithmType.Brotli
        );

        var packet = new TestBigPacket()
        {
            Content = (await File.ReadAllBytesAsync(
                Path.Combine(Directory.GetCurrentDirectory(), "MockAssets", "large-file.json")
            )).ToList()
        };

        var encodedPacket = await encoderDecoder.EncodeAsync(packet);

        Assert.That(encodedPacket, Is.Not.Null);

        var decodedPacket = await encoderDecoder.DecodeAsync(encodedPacket);

        Assert.That(decodedPacket, Is.Not.Null);
    }
}
