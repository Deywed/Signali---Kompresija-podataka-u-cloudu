```

BenchmarkDotNet v0.15.8, macOS Tahoe 26.3 (25D125) [Darwin 25.3.0]
Apple M1 Pro, 1 CPU, 8 logical and 8 physical cores
.NET SDK 10.0.102
  [Host]     : .NET 10.0.2 (10.0.2, 10.0.225.61305), Arm64 RyuJIT armv8.0-a
  DefaultJob : .NET 10.0.2 (10.0.2, 10.0.225.61305), Arm64 RyuJIT armv8.0-a


```
| Method                   | FileName     | Mean       | Error     | StdDev    | Median     | Gen0      | Gen1     | Gen2     | Allocated |
|------------------------- |------------- |-----------:|----------:|----------:|-----------:|----------:|---------:|---------:|----------:|
| **&#39;GZIP - Compression&#39;**     | **Data/dickens** | **181.050 ms** | **2.0046 ms** | **1.7770 ms** | **180.869 ms** |         **-** |        **-** |        **-** |  **11.71 MB** |
| &#39;Zstd - Compression&#39;     | Data/dickens |  73.498 ms | 1.4373 ms | 2.5918 ms |  73.122 ms |         - |        - |        - |  11.41 MB |
| &#39;LZ4 - Compression&#39;      | Data/dickens |  40.870 ms | 0.7976 ms | 1.0371 ms |  40.631 ms |  153.8462 | 153.8462 | 153.8462 |  26.49 MB |
| &#39;Snappy - Compression&#39;   | Data/dickens |  55.261 ms | 0.5325 ms | 0.4981 ms |  55.043 ms |  800.0000 |        - |        - |  10.93 MB |
| &#39;Brotli - Compression&#39;   | Data/dickens | 135.103 ms | 2.6395 ms | 4.1093 ms | 133.069 ms |         - |        - |        - |  11.22 MB |
| &#39;GZIP - Decompression&#39;   | Data/dickens |  30.112 ms | 0.5098 ms | 0.4769 ms |  30.229 ms |  593.7500 | 593.7500 | 593.7500 |   41.6 MB |
| &#39;Zstd - Decompression&#39;   | Data/dickens |  19.938 ms | 0.3886 ms | 0.3991 ms |  19.955 ms |  593.7500 | 593.7500 | 593.7500 |   41.6 MB |
| &#39;LZ4 - Decompression&#39;    | Data/dickens |  11.277 ms | 0.2253 ms | 0.4752 ms |  11.164 ms |  453.1250 | 453.1250 | 453.1250 |   41.6 MB |
| &#39;Snappy - Decompression&#39; | Data/dickens |  27.962 ms | 0.2202 ms | 0.2059 ms |  27.979 ms |   62.5000 |  62.5000 |  62.5000 |   9.72 MB |
| &#39;Brotli - Decompression&#39; | Data/dickens |  29.718 ms | 0.4017 ms | 0.3758 ms |  29.784 ms |  437.5000 | 437.5000 | 437.5000 |   41.6 MB |
| **&#39;GZIP - Compression&#39;**     | **Data/nci**     | **202.649 ms** | **0.6536 ms** | **0.5794 ms** | **202.599 ms** |         **-** |        **-** |        **-** |  **10.99 MB** |
| &#39;Zstd - Compression&#39;     | Data/nci     |  63.087 ms | 0.5765 ms | 0.5393 ms |  62.900 ms |         - |        - |        - |  10.63 MB |
| &#39;LZ4 - Compression&#39;      | Data/nci     |  45.032 ms | 0.6735 ms | 0.5971 ms |  44.887 ms |         - |        - |        - |  23.92 MB |
| &#39;Snappy - Compression&#39;   | Data/nci     |  54.834 ms | 0.1370 ms | 0.1144 ms |  54.798 ms | 2555.5556 |        - |        - |  21.87 MB |
| &#39;Brotli - Compression&#39;   | Data/nci     | 133.046 ms | 2.3719 ms | 1.9806 ms | 132.363 ms |         - |        - |        - |  10.45 MB |
| &#39;GZIP - Decompression&#39;   | Data/nci     |  39.153 ms | 0.2811 ms | 0.2630 ms |  39.067 ms |  307.6923 | 307.6923 | 307.6923 |  95.87 MB |
| &#39;Zstd - Decompression&#39;   | Data/nci     |  31.809 ms | 0.3685 ms | 0.3266 ms |  31.718 ms |  250.0000 | 250.0000 | 250.0000 |  95.87 MB |
| &#39;LZ4 - Decompression&#39;    | Data/nci     |  29.046 ms | 0.3845 ms | 0.3597 ms |  29.052 ms |  250.0000 | 250.0000 | 250.0000 |  95.88 MB |
| &#39;Snappy - Decompression&#39; | Data/nci     |  27.074 ms | 0.1294 ms | 0.1210 ms |  27.062 ms |   62.5000 |  62.5000 |  62.5000 |     32 MB |
| &#39;Brotli - Decompression&#39; | Data/nci     |  36.401 ms | 0.2535 ms | 0.2372 ms |  36.469 ms |  250.0000 | 250.0000 | 250.0000 |  95.87 MB |
| **&#39;GZIP - Compression&#39;**     | **Data/reymont** | **124.828 ms** | **0.6524 ms** | **0.5448 ms** | **124.791 ms** |         **-** |        **-** |        **-** |   **5.78 MB** |
| &#39;Zstd - Compression&#39;     | Data/reymont |  35.180 ms | 0.1725 ms | 0.1529 ms |  35.158 ms |         - |        - |        - |   5.75 MB |
| &#39;LZ4 - Compression&#39;      | Data/reymont |  24.539 ms | 0.1318 ms | 0.1169 ms |  24.554 ms |  312.5000 | 312.5000 | 312.5000 |  10.79 MB |
| &#39;Snappy - Compression&#39;   | Data/reymont |  29.376 ms | 0.2677 ms | 0.2504 ms |  29.327 ms |  531.2500 |  31.2500 |  31.2500 |   6.27 MB |
| &#39;Brotli - Compression&#39;   | Data/reymont |  68.514 ms | 1.0786 ms | 1.0089 ms |  68.566 ms |         - |        - |        - |   5.62 MB |
| &#39;GZIP - Decompression&#39;   | Data/reymont |  16.587 ms | 0.1130 ms | 0.1057 ms |  16.603 ms |  375.0000 | 375.0000 | 375.0000 |   22.2 MB |
| &#39;Zstd - Decompression&#39;   | Data/reymont |  11.240 ms | 0.0393 ms | 0.0348 ms |  11.234 ms |  359.3750 | 359.3750 | 359.3750 |   22.2 MB |
| &#39;LZ4 - Decompression&#39;    | Data/reymont |   7.432 ms | 0.0313 ms | 0.0277 ms |   7.429 ms |  367.1875 | 367.1875 | 367.1875 |   22.2 MB |
| &#39;Snappy - Decompression&#39; | Data/reymont |  15.754 ms | 0.0310 ms | 0.0275 ms |  15.750 ms |   93.7500 |  93.7500 |  93.7500 |   6.32 MB |
| &#39;Brotli - Decompression&#39; | Data/reymont |  15.394 ms | 0.0344 ms | 0.0305 ms |  15.386 ms |  359.3750 | 359.3750 | 359.3750 |   22.2 MB |
