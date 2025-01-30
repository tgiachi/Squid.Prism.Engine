using System.IO.Compression;
using Squid.Prism.Network.Types;

namespace Squid.Prism.Network.Utils;

public static class CompressionUtils
{
    public static async Task<byte[]> CompressBrotli(byte[] bytes)
    {
        await using var input = new MemoryStream(bytes);
        await using var output = new MemoryStream();
        await using var brotliStream = new BrotliStream(output, CompressionLevel.Fastest);

        await input.CopyToAsync(brotliStream);
        await brotliStream.FlushAsync();

        return output.ToArray();
    }

    public static async Task<byte[]> DecompressBrotli(byte[] compressed)
    {
        await using var input = new MemoryStream(compressed);
        await using var brotliStream = new BrotliStream(input, CompressionMode.Decompress);

        await using var output = new MemoryStream();

        await brotliStream.CopyToAsync(output);
        await brotliStream.FlushAsync();

        return output.ToArray();
    }

    public static async Task<byte[]> CompressGZip(byte[] bytes)
    {
        await using var input = new MemoryStream(bytes);
        await using var output = new MemoryStream();
        await using var gzipStream = new GZipStream(output, CompressionLevel.Fastest);

        await input.CopyToAsync(gzipStream);
        await gzipStream.FlushAsync();

        return output.ToArray();
    }

    public static async Task<byte[]> DecompressGZip(byte[] compressed)
    {
        await using var input = new MemoryStream(compressed);
        await using var gzipStream = new GZipStream(input, CompressionMode.Decompress);
        await using var output = new MemoryStream();

        await gzipStream.CopyToAsync(output);
        await gzipStream.FlushAsync();

        return output.ToArray();
    }

    public static async Task<byte[]> CompressDeflate(byte[] bytes)
    {
        await using var input = new MemoryStream(bytes);
        await using var output = new MemoryStream();
        await using var deflateStream = new DeflateStream(output, CompressionLevel.Fastest);

        await input.CopyToAsync(deflateStream);
        await deflateStream.FlushAsync();

        return output.ToArray();
    }

    public static async Task<byte[]> DecompressDeflate(byte[] compressed)
    {
        await using var input = new MemoryStream(compressed);
        await using var deflateStream = new DeflateStream(input, CompressionMode.Decompress);
        await using var output = new MemoryStream();

        await deflateStream.CopyToAsync(output);
        await deflateStream.FlushAsync();

        return output.ToArray();
    }

    public static async Task<byte[]> Compress(
        byte[] bytes, CompressionAlgorithmType algorithm = CompressionAlgorithmType.Brotli
    )
    {
        return algorithm switch
        {
            CompressionAlgorithmType.Brotli  => await CompressBrotli(bytes),
            CompressionAlgorithmType.GZip    => await CompressGZip(bytes),
            CompressionAlgorithmType.Deflate => await CompressDeflate(bytes),
            _                                => throw new ArgumentException("Not supported", nameof(algorithm))
        };
    }

    public static async Task<byte[]> Decompress(
        byte[] compressed, CompressionAlgorithmType algorithm = CompressionAlgorithmType.Brotli
    )
    {
        return algorithm switch
        {
            CompressionAlgorithmType.Brotli  => await DecompressBrotli(compressed),
            CompressionAlgorithmType.GZip    => await DecompressGZip(compressed),
            CompressionAlgorithmType.Deflate => await DecompressDeflate(compressed),
            _                                => throw new ArgumentException("Not supported", nameof(algorithm))
        };
    }
}
