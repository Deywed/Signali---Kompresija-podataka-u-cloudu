```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.3 (25D125) [Darwin 25.3.0]
Apple M1 Pro, 1 CPU, 8 logical and 8 physical cores
.NET SDK 10.0.102
  [Host]     : .NET 10.0.2 (10.0.2, 10.0.225.61305), Arm64 RyuJIT armv8.0-a
  DefaultJob : .NET 10.0.2 (10.0.2, 10.0.225.61305), Arm64 RyuJIT armv8.0-a


```
| Method                   | Mean       | Error     | StdDev    | Gen0     | Gen1     | Gen2     | Allocated |
|------------------------- |-----------:|----------:|----------:|---------:|---------:|---------:|----------:|
| &#39;GZIP - Compression&#39;     | 138.846 ms | 2.4863 ms | 2.0761 ms |        - |        - |        - |  11.71 MB |
| &#39;Zstd - Compression&#39;     |  55.350 ms | 0.9769 ms | 0.8158 ms |        - |        - |        - |  11.41 MB |
| &#39;LZ4 - Compression&#39;      |  32.323 ms | 0.6173 ms | 0.5472 ms | 181.8182 | 181.8182 | 181.8182 |  26.49 MB |
| &#39;Snappy - Compression&#39;   |  42.912 ms | 0.4705 ms | 0.4171 ms | 750.0000 |        - |        - |  10.93 MB |
| &#39;Brotli - Compression&#39;   | 121.055 ms | 1.9951 ms | 1.6660 ms |        - |        - |        - |  11.22 MB |
| &#39;GZIP - Decompression&#39;   |  25.551 ms | 0.4915 ms | 0.5463 ms | 500.0000 | 500.0000 | 500.0000 |   41.6 MB |
| &#39;Zstd - Decompression&#39;   |  17.229 ms | 0.3423 ms | 0.8266 ms | 468.7500 | 468.7500 | 468.7500 |   41.6 MB |
| &#39;LZ4 - Decompression&#39;    |   9.334 ms | 0.1824 ms | 0.2617 ms | 453.1250 | 453.1250 | 453.1250 |   41.6 MB |
| &#39;Snappy - Decompression&#39; |  22.659 ms | 0.4522 ms | 0.6036 ms |  62.5000 |  62.5000 |  62.5000 |   9.72 MB |
| &#39;Brotli - Decompression&#39; |  24.667 ms | 0.4926 ms | 0.5059 ms | 437.5000 | 437.5000 | 437.5000 |   41.6 MB |
