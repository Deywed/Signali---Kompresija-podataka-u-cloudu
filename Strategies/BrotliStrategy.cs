using System.IO;
using System.IO.Compression;
using CompressionBenchmark.Abstractions;

namespace CompressionBenchmark.Strategies
{
    public class BrotliStrategy : ICompressionStrategy
    {
        public string Name => "Brotli";

        public byte[] Compress(byte[] data)
        {
            using var outputStream = new MemoryStream();
            // Koristimo fabrički BrotliStream iz .NET-a
            using (var brotliStream = new BrotliStream(outputStream, CompressionLevel.Optimal, leaveOpen: true))
            {
                brotliStream.Write(data, 0, data.Length);
            }
            return outputStream.ToArray();
        }

        public byte[] Decompress(byte[] compressedData)
        {
            using var inputStream = new MemoryStream(compressedData);
            using var brotliStream = new BrotliStream(inputStream, CompressionMode.Decompress);
            using var outputStream = new MemoryStream();

            brotliStream.CopyTo(outputStream);
            return outputStream.ToArray();
        }
    }
}