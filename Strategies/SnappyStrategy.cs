using IronSnappy;
using CompressionBenchmark.Abstractions;

namespace CompressionBenchmark.Strategies
{
    public class SnappyStrategy : ICompressionStrategy
    {
        public string Name => "Snappy";

        public byte[] Compress(byte[] data)
        {
            // Snappy omogućava direktnu kompresiju bajtova bez eksplicitnog otvaranja MemoryStream-a
            return Snappy.Encode(data);
        }

        public byte[] Decompress(byte[] compressedData)
        {
            // Direktna dekompresija u jednom koraku
            return Snappy.Decode(compressedData);
        }
    }
}