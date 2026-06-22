using System;
using System.IO;
using BenchmarkDotNet.Attributes;
using CompressionBenchmark.Abstractions;
using CompressionBenchmark.Strategies;

namespace CompressionBenchmark.Benchmarks
{
    [MemoryDiagnoser] // Prati potrošnju RAM memorije
    [HtmlExporter]    // Generiše uporedni HTML fajl
    [MarkdownExporterAttribute.GitHub] // Generiše .md fajl spreman za GitHub/rad
    public class CompressionPerformanceBenchmark
    {
        private byte[] _testData = null!;

        // Unapred kompresovani podaci koji nam trebaju za test DEKOMRESIJE
        private byte[] _gzipCompressed = null!;
        private byte[] _zstdCompressed = null!;
        private byte[] _lz4Compressed = null!;
        private byte[] _snappyCompressed = null!;
        private byte[] _brotliCompressed = null!;

        // Instanciranje svih strategija na jednom mestu
        private readonly ICompressionStrategy _gzip = new GzipStrategy();
        private readonly ICompressionStrategy _zstd = new ZstdStrategy();
        private readonly ICompressionStrategy _lz4 = new Lz4Strategy();
        private readonly ICompressionStrategy _snappy = new SnappyStrategy();
        private readonly ICompressionStrategy _brotli = new BrotliStrategy();

        [GlobalSetup]
        public void Setup()
        {
            // Pošto smo u .csproj podesili CopyToOutputDirectory, 
            // fajl je sada garantovano pored izvršnog programa!
            _testData = File.ReadAllBytes("Data/dickens"); // Učitavanje test fajla u bajt niz

            // Priprema kompresovanih nizova bajtova za test dekompresije
            _gzipCompressed = _gzip.Compress(_testData);
            _zstdCompressed = _zstd.Compress(_testData);
            _lz4Compressed = _lz4.Compress(_testData);
            _snappyCompressed = _snappy.Compress(_testData);
            _brotliCompressed = _brotli.Compress(_testData);
        }

        // --- BENCHMARK TESTOVI ZA KOMPRESIJU ---

        [Benchmark(Description = "GZIP - Compression")]
        public byte[] GzipCompress() => _gzip.Compress(_testData);

        [Benchmark(Description = "Zstd - Compression")]
        public byte[] ZstdCompress() => _zstd.Compress(_testData);

        [Benchmark(Description = "LZ4 - Compression")]
        public byte[] Lz4Compress() => _lz4.Compress(_testData);

        [Benchmark(Description = "Snappy - Compression")]
        public byte[] SnappyCompress() => _snappy.Compress(_testData);

        [Benchmark(Description = "Brotli - Compression")]
        public byte[] BrotliCompress() => _brotli.Compress(_testData);


        // --- BENCHMARK TESTOVI ZA DEKOMPRESIJU ---

        [Benchmark(Description = "GZIP - Decompression")]
        public byte[] GzipDecompress() => _gzip.Decompress(_gzipCompressed);

        [Benchmark(Description = "Zstd - Decompression")]
        public byte[] ZstdDecompress() => _zstd.Decompress(_zstdCompressed);

        [Benchmark(Description = "LZ4 - Decompression")]
        public byte[] Lz4Decompress() => _lz4.Decompress(_lz4Compressed);

        [Benchmark(Description = "Snappy - Decompression")]
        public byte[] SnappyDecompress() => _snappy.Decompress(_snappyCompressed);

        [Benchmark(Description = "Brotli - Decompression")]
        public byte[] BrotliDecompress() => _brotli.Decompress(_brotliCompressed);
    }
}