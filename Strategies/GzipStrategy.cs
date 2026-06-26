using System.IO.Compression;
using CompressionBenchmark.Abstractions;

namespace CompressionBenchmark.Strategies
{
    public class GzipStrategy : ICompressionStrategy
    {
        // GZipStream(.., CompressionMode.Compress) interno koristi
        // CompressionLevel.Optimal (najjača kompresija, sporiji rad).
        public string Name => "Gzip (Optimal)";

        public byte[] Compress(byte[] data)
        {
            using var outputStream = new MemoryStream();
            using (var gzipStream = new GZipStream(outputStream, CompressionMode.Compress,
            leaveOpen: true))
            {
                gzipStream.Write(data, 0, data.Length);
            }
            return outputStream.ToArray();
        }

        public byte[] Decompress(byte[] compressedData)
        {
            using var inputStream = new MemoryStream(compressedData);
            using var gzipStream = new GZipStream(inputStream,
            CompressionMode.Decompress);
            using var outputStream = new MemoryStream();
            gzipStream.CopyTo(outputStream);
            return outputStream.ToArray();
        }
    }
}