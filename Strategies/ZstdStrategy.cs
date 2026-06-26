using ZstdSharp; // Novi, moderni namespace
using CompressionBenchmark.Abstractions;

namespace CompressionBenchmark.Strategies
{
    public class ZstdStrategy : ICompressionStrategy
    {
        // Zstd nivo 3 je standardni default (balans brzine i odnosa kompresije).
        public string Name => $"Zstandard (Level {_compressionLevel})";
        private readonly int _compressionLevel;

        public ZstdStrategy(int compressionLevel = 3)
        {
            _compressionLevel = compressionLevel;
        }

        public byte[] Compress(byte[] data)
        {
            using var outputStream = new MemoryStream();
            using (var compressionStream = new CompressionStream(outputStream, _compressionLevel, leaveOpen: true))
            {
                compressionStream.Write(data, 0, data.Length);
            }
            return outputStream.ToArray();
        }

        public byte[] Decompress(byte[] compressedData)
        {
            using var inputStream = new MemoryStream(compressedData);
            using var decompressionStream = new DecompressionStream(inputStream);
            using var outputStream = new MemoryStream();

            decompressionStream.CopyTo(outputStream);
            return outputStream.ToArray();
        }
    }
}