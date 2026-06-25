```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.3 (25D125) [Darwin 25.3.0]
Apple M1 Pro, 1 CPU, 8 logical and 8 physical cores
.NET SDK 10.0.102
  [Host]     : .NET 10.0.2 (10.0.2, 10.0.225.61305), Arm64 RyuJIT armv8.0-a
  DefaultJob : .NET 10.0.2 (10.0.2, 10.0.225.61305), Arm64 RyuJIT armv8.0-a


```
| Method                   | FileName     | Mean       | Error     | StdDev    | Median     | Gen0      | Gen1     | Gen2     | Allocated |
|------------------------- |------------- |-----------:|----------:|----------:|-----------:|----------:|---------:|---------:|----------:|
| **&#39;GZIP - Compression&#39;**     | **Data/dickens** | **144.972 ms** | **2.8907 ms** | **5.1383 ms** | **142.883 ms** |         **-** |        **-** |        **-** |  **11.71 MB** |
| &#39;Zstd - Compression&#39;     | Data/dickens |  56.373 ms | 0.3425 ms | 0.3204 ms |  56.395 ms |         - |        - |        - |  11.41 MB |
| &#39;LZ4 - Compression&#39;      | Data/dickens |  31.947 ms | 0.3761 ms | 0.3518 ms |  32.032 ms |  250.0000 | 250.0000 | 250.0000 |  26.49 MB |
| &#39;Snappy - Compression&#39;   | Data/dickens |  43.082 ms | 0.7082 ms | 0.6278 ms |  42.892 ms |  750.0000 |        - |        - |  10.93 MB |
| &#39;Brotli - Compression&#39;   | Data/dickens | 114.246 ms | 2.1989 ms | 5.0080 ms | 112.043 ms |         - |        - |        - |  11.22 MB |
| &#39;GZIP - Decompression&#39;   | Data/dickens |  24.279 ms | 0.2417 ms | 0.2019 ms |  24.338 ms |  437.5000 | 437.5000 | 437.5000 |   41.6 MB |
| &#39;Zstd - Decompression&#39;   | Data/dickens |  17.898 ms | 0.3545 ms | 1.0396 ms |  17.930 ms |  468.7500 | 468.7500 | 468.7500 |   41.6 MB |
| &#39;LZ4 - Decompression&#39;    | Data/dickens |   9.874 ms | 0.1974 ms | 0.3509 ms |   9.876 ms |  453.1250 | 453.1250 | 453.1250 |   41.6 MB |
| &#39;Snappy - Decompression&#39; | Data/dickens |  21.882 ms | 0.0599 ms | 0.0531 ms |  21.878 ms |   62.5000 |  62.5000 |  62.5000 |   9.72 MB |
| &#39;Brotli - Decompression&#39; | Data/dickens |  25.110 ms | 0.2169 ms | 0.2029 ms |  25.051 ms |  500.0000 | 500.0000 | 500.0000 |   41.6 MB |
| **&#39;GZIP - Compression&#39;**     | **Data/nci**     | **160.667 ms** | **1.1779 ms** | **1.0442 ms** | **160.411 ms** |         **-** |        **-** |        **-** |  **10.99 MB** |
| &#39;Zstd - Compression&#39;     | Data/nci     |  52.357 ms | 0.1747 ms | 0.1549 ms |  52.362 ms |         - |        - |        - |  10.63 MB |
| &#39;LZ4 - Compression&#39;      | Data/nci     |  35.581 ms | 0.1639 ms | 0.1453 ms |  35.537 ms |         - |        - |        - |  23.92 MB |
| &#39;Snappy - Compression&#39;   | Data/nci     |  45.471 ms | 0.8872 ms | 1.0562 ms |  45.130 ms | 2583.3333 |        - |        - |  21.87 MB |
| &#39;Brotli - Compression&#39;   | Data/nci     | 118.518 ms | 2.0162 ms | 2.2410 ms | 118.096 ms |         - |        - |        - |  10.45 MB |
| &#39;GZIP - Decompression&#39;   | Data/nci     |  33.064 ms | 0.3272 ms | 0.2732 ms |  33.097 ms |  375.0000 | 375.0000 | 375.0000 |  95.87 MB |
| &#39;Zstd - Decompression&#39;   | Data/nci     |  27.956 ms | 0.2833 ms | 0.2511 ms |  27.931 ms |  437.5000 | 437.5000 | 437.5000 |  95.87 MB |
| &#39;LZ4 - Decompression&#39;    | Data/nci     |  25.411 ms | 0.1861 ms | 0.1453 ms |  25.403 ms |  437.5000 | 437.5000 | 437.5000 |  95.88 MB |
| &#39;Snappy - Decompression&#39; | Data/nci     |  21.859 ms | 0.1879 ms | 0.1666 ms |  21.785 ms |  125.0000 | 125.0000 | 125.0000 |     32 MB |
| &#39;Brotli - Decompression&#39; | Data/nci     |  31.526 ms | 0.5349 ms | 0.4467 ms |  31.557 ms |  500.0000 | 500.0000 | 500.0000 |  95.87 MB |
| **&#39;GZIP - Compression&#39;**     | **Data/reymont** |  **98.993 ms** | **0.3745 ms** | **0.2924 ms** |  **98.986 ms** |         **-** |        **-** |        **-** |   **5.78 MB** |
| &#39;Zstd - Compression&#39;     | Data/reymont |  29.122 ms | 0.5658 ms | 0.5557 ms |  28.968 ms |   62.5000 |  62.5000 |  62.5000 |   5.75 MB |
| &#39;LZ4 - Compression&#39;      | Data/reymont |  19.343 ms | 0.1618 ms | 0.1263 ms |  19.378 ms |  312.5000 | 312.5000 | 312.5000 |  10.79 MB |
| &#39;Snappy - Compression&#39;   | Data/reymont |  23.953 ms | 0.2850 ms | 0.2666 ms |  23.971 ms |  531.2500 |  31.2500 |  31.2500 |   6.27 MB |
| &#39;Brotli - Compression&#39;   | Data/reymont |  59.035 ms | 0.3398 ms | 0.3178 ms |  59.116 ms |         - |        - |        - |   5.62 MB |
| &#39;GZIP - Decompression&#39;   | Data/reymont |  13.773 ms | 0.0502 ms | 0.0445 ms |  13.774 ms |  500.0000 | 500.0000 | 500.0000 |   22.2 MB |
| &#39;Zstd - Decompression&#39;   | Data/reymont |   9.831 ms | 0.1110 ms | 0.0984 ms |   9.838 ms |  359.3750 | 359.3750 | 359.3750 |   22.2 MB |
| &#39;LZ4 - Decompression&#39;    | Data/reymont |   6.629 ms | 0.0799 ms | 0.0747 ms |   6.654 ms |  359.3750 | 359.3750 | 359.3750 |   22.2 MB |
| &#39;Snappy - Decompression&#39; | Data/reymont |  12.736 ms | 0.1170 ms | 0.1037 ms |  12.712 ms |  109.3750 | 109.3750 | 109.3750 |   6.32 MB |
| &#39;Brotli - Decompression&#39; | Data/reymont |  13.530 ms | 0.0918 ms | 0.0814 ms |  13.524 ms |  359.3750 | 359.3750 | 359.3750 |   22.2 MB |
