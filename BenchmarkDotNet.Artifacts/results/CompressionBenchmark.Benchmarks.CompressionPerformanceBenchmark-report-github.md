```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.3 (25D125) [Darwin 25.3.0]
Apple M1 Pro, 1 CPU, 8 logical and 8 physical cores
.NET SDK 10.0.102
  [Host]     : .NET 10.0.2 (10.0.2, 10.0.225.61305), Arm64 RyuJIT armv8.0-a
  DefaultJob : .NET 10.0.2 (10.0.2, 10.0.225.61305), Arm64 RyuJIT armv8.0-a


```
| Method                   | FileName      | Mean      | Error    | StdDev   | Gen0      | Gen1     | Gen2     | Allocated |
|------------------------- |-------------- |----------:|---------:|---------:|----------:|---------:|---------:|----------:|
| &#39;GZIP - Compression&#39;     | Data/20mb.csv | 379.18 ms | 2.226 ms | 2.082 ms |         - |        - |        - |  41.81 MB |
| &#39;Zstd - Compression&#39;     | Data/20mb.csv | 165.18 ms | 0.322 ms | 0.286 ms |         - |        - |        - |  41.31 MB |
| &#39;LZ4 - Compression&#39;      | Data/20mb.csv |  73.17 ms | 0.151 ms | 0.134 ms |  285.7143 | 285.7143 | 285.7143 |  69.69 MB |
| &#39;Snappy - Compression&#39;   | Data/20mb.csv |  96.25 ms | 0.102 ms | 0.096 ms | 1666.6667 |        - |        - |  25.97 MB |
| &#39;Brotli - Compression&#39;   | Data/20mb.csv | 289.89 ms | 0.554 ms | 0.518 ms |         - |        - |        - |  40.77 MB |
| &#39;GZIP - Decompression&#39;   | Data/20mb.csv |  82.72 ms | 0.212 ms | 0.188 ms |  285.7143 | 285.7143 | 285.7143 |  83.97 MB |
| &#39;Zstd - Decompression&#39;   | Data/20mb.csv |  40.35 ms | 0.124 ms | 0.110 ms |  307.6923 | 307.6923 | 307.6923 |  83.97 MB |
| &#39;LZ4 - Decompression&#39;    | Data/20mb.csv |  22.82 ms | 0.057 ms | 0.051 ms |  250.0000 | 250.0000 | 250.0000 |  83.97 MB |
| &#39;Snappy - Decompression&#39; | Data/20mb.csv |  45.23 ms | 0.054 ms | 0.050 ms |         - |        - |        - |   20.1 MB |
| &#39;Brotli - Decompression&#39; | Data/20mb.csv |  75.33 ms | 0.193 ms | 0.171 ms |  285.7143 | 285.7143 | 285.7143 |  81.59 MB |
