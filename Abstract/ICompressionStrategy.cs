using System;
namespace CompressionBenchmark.Abstractions
{
    public interface ICompressionStrategy
    {
        string Name { get; }
        byte[] Compress(byte[] data);
        byte[] Decompress(byte[] compressedData);
    }
}