using System;
using System.IO;
using BenchmarkDotNet.Attributes;
using CompressionBenchmark.Abstractions;
using CompressionBenchmark.Strategies;

namespace CompressionBenchmark.Benchmarks
{
    [MemoryDiagnoser]
    [HtmlExporter]
    [MarkdownExporterAttribute.GitHub]
    public class CompressionPerformanceBenchmark
    {
        private byte[] _testData = null!;

        // Unapred kompresovani podaci za test DEKOMRESIJE
        private byte[] _gzipCompressed = null!;
        private byte[] _zstdCompressed = null!;
        private byte[] _lz4Compressed = null!;
        private byte[] _snappyCompressed = null!;
        private byte[] _brotliCompressed = null!;

        // Instanciranje strategija
        private readonly ICompressionStrategy _gzip = new GzipStrategy();
        private readonly ICompressionStrategy _zstd = new ZstdStrategy();
        private readonly ICompressionStrategy _lz4 = new Lz4Strategy();
        private readonly ICompressionStrategy _snappy = new SnappyStrategy();
        private readonly ICompressionStrategy _brotli = new BrotliStrategy();

        // 1. OVDE ZADAJEŠ SVE FAJLOVE KOJE ŽELIŠ DA TESTIRAŠ ODJEDNOM
        // BenchmarkDotNet će pročitati ove nazive i izvršiti testove za svaki ponaosob
        [Params("Data/dickens", "Data/nci", "Data/reymont")]
        public string FileName { get; set; } = null!;

        [GlobalSetup]
        public void Setup()
        {
            // 2. Putanja se sada dinamički menja u zavisnosti od toga koji fajl je trenutno na redu
            if (!File.Exists(FileName))
            {
                throw new FileNotFoundException($"Fajl nije pronađen na lokaciji: {FileName}. Proveri da li je u Data folderu.");
            }

            _testData = File.ReadAllBytes(FileName);

            // Priprema kompresovanih nizova bajtova za trenutno aktivni fajl
            _gzipCompressed = _gzip.Compress(_testData);
            _zstdCompressed = _zstd.Compress(_testData);
            _lz4Compressed = _lz4.Compress(_testData);
            _snappyCompressed = _snappy.Compress(_testData);
            _brotliCompressed = _brotli.Compress(_testData);
        }

        // --- BENCHMARK TESTOVI ZA KOMPRESIJU ---
        // Kod za metode ostaje POTPUNO ISTI, jer sve one koriste _testData koji se gore dinamički menja

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