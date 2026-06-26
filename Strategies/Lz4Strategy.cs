using K4os.Compression.LZ4;
using K4os.Compression.LZ4.Streams;
using CompressionBenchmark.Abstractions;

namespace CompressionBenchmark.Strategies
{
    public class Lz4Strategy : ICompressionStrategy
    {
        // L00_FAST: najbrži nivo, najmanji odnos kompresije.
        public string Name => "LZ4 (Fast)";

        public byte[] Compress(byte[] data)
        {
            using var outputStream = new MemoryStream();

            // Definišemo LZ4 taster za kompresiju sa podešavanjem za maksimalnu brzinu
            var settings = new LZ4EncoderSettings { CompressionLevel = LZ4Level.L00_FAST };

            using (var lz4Stream = LZ4Stream.Encode(outputStream, settings, leaveOpen: true))
            {
                lz4Stream.Write(data, 0, data.Length);
            }
            return outputStream.ToArray();
        }

        public byte[] Decompress(byte[] compressedData)
        {
            using var inputStream = new MemoryStream(compressedData);
            using var lz4Stream = LZ4Stream.Decode(inputStream);
            using var outputStream = new MemoryStream();

            lz4Stream.CopyTo(outputStream);
            return outputStream.ToArray();
        }
    }
}