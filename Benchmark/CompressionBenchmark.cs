using BenchmarkDotNet.Attributes;
using CompressionBenchmark.Abstractions;
using CompressionBenchmark.Strategies;
using CompressionBenchmark.Utils;

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

        // BenchmarkDotNet će pročitati ove nazive i izvršiti testove za svaki ponaosob
        // Testiramo samo novi fajl (stari su već izmereni).
        // Da vratiš sve nazad, dodaj: "Data/dickens", "Data/nci", "Data/reymont"
        [Params("Data/20mb.csv")]
        public string FileName { get; set; } = null!;

        [GlobalSetup]
        public void Setup()
        {
            //    BenchmarkDotNet procesa), nezavisno od trenutnog working direktorijuma.
            string path = DataLocator.ResolveDataFile(FileName);
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"Fajl nije pronađen na lokaciji: {path}. Proveri da li je u Data folderu.");
            }

            _testData = File.ReadAllBytes(path);

            // Priprema kompresovanih nizova bajtova za trenutno aktivni fajl
            _gzipCompressed = _gzip.Compress(_testData);
            _zstdCompressed = _zstd.Compress(_testData);
            _lz4Compressed = _lz4.Compress(_testData);
            _snappyCompressed = _snappy.Compress(_testData);
            _brotliCompressed = _brotli.Compress(_testData);

            // Validacija korektnosti: dekompresovani podaci moraju biti identični
            // originalu. Ako neki algoritam pokvari podatke, test puca odmah u Setup-u
            // umesto da merimo brzinu nečega što ne radi ispravno.
            VerifyRoundTrip(_gzip, _gzipCompressed);
            VerifyRoundTrip(_zstd, _zstdCompressed);
            VerifyRoundTrip(_lz4, _lz4Compressed);
            VerifyRoundTrip(_snappy, _snappyCompressed);
            VerifyRoundTrip(_brotli, _brotliCompressed);
        }

        private void VerifyRoundTrip(ICompressionStrategy strategy, byte[] compressed)
        {
            byte[] restored = strategy.Decompress(compressed);
            if (!restored.AsSpan().SequenceEqual(_testData))
            {
                throw new InvalidOperationException(
                    $"Round-trip provera nije uspela za {strategy.Name}: dekompresovani podaci se razlikuju od originala.");
            }
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