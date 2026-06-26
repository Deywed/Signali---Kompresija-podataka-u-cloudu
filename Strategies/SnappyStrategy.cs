using IronSnappy;
using CompressionBenchmark.Abstractions;

namespace CompressionBenchmark.Strategies
{
    public class SnappyStrategy : ICompressionStrategy
    {
        public string Name => "Snappy";

        public byte[] Compress(byte[] data)
        {
            return Snappy.Encode(data);
        }

        public byte[] Decompress(byte[] compressedData)
        {
            return Snappy.Decode(compressedData);
        }
    }
}