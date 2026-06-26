# Kompresija podataka u cloudu — benchmark

Projekat poredi pet algoritama za kompresiju podataka po dve ose:

1. **Performanse** — brzina kompresije/dekompresije i alokacije memorije (mereno preko [BenchmarkDotNet](https://benchmarkdotnet.org/)).
2. **Trošak u cloudu** — procena uštede na skladištenju (AWS S3) i mrežnom prenosu (egress) kada se podaci čuvaju u kompresovanom obliku.

## Algoritmi

| Algoritam | Biblioteka | Nivo |
|-----------|------------|------|
| Gzip | `System.IO.Compression` | Optimal |
| Zstandard | `ZstdSharp.Port` | Level 3 (default) |
| LZ4 | `K4os.Compression.LZ4` | L00_FAST |
| Snappy | `IronSnappy` | — (fiksni) |
| Brotli | `System.IO.Compression` | Optimal |

> **Napomena o poređenju:** algoritmi rade na svojim tipičnim default nivoima, koji nisu
> međusobno uporedivi po "naporu". Gzip i Brotli koriste `Optimal` (najjača, najsporija
> kompresija), LZ4 najbrži nivo, a Zstd umereni balans. Zato treba čitati i odnos
> kompresije (ratio) zajedno sa brzinom, a ne brzinu izolovano.

## Arhitektura

- `Abstract/ICompressionStrategy.cs` — zajednički interfejs (`Compress` / `Decompress` / `Name`).
- `Strategies/` — implementacija po algoritmu (Strategy pattern).
- `Benchmark/CompressionBenchmark.cs` — BenchmarkDotNet testovi; u `GlobalSetup` se radi
  i provera korektnosti (round-trip: `Decompress(Compress(x)) == x`).
- `Utils/CloudCostCalculator.cs` — proračun troškova skladištenja i prenosa.
- `Utils/DataLocator.cs` — robustno pronalaženje `Data` foldera nezavisno od working direktorijuma.
- `Models/CostEstimationResult.cs` — rezultat finansijske procene.
- `Program.cs` — pokreće benchmark, pa generiše `cloud_cost_report.txt`.

## Testni podaci

Fajlovi `dickens`, `nci`, `reymont` (i `samba`) su deo [Silesia korpusa](http://sun.aei.polsl.pl/~sdeor/index.php?page=silesia),
standardnog skupa za testiranje kompresije. Smešteni su u `Data/` i kopiraju se u
izlazni folder pri build-u.

## Pokretanje

```bash
dotnet run -c Release
```

> BenchmarkDotNet zahteva **Release** konfiguraciju. Pokretanje punog benchmark-a traje
> nekoliko minuta. Rezultati performansi se upisuju u `BenchmarkDotNet.Artifacts/`, a
> finansijski izveštaj u `cloud_cost_report.txt`.

## Pretpostavke za proračun troškova

- Skladištenje (AWS S3 Standard): **$0.023 / GB / mesec**
- Mrežni prenos (egress na internet): **$0.09 / GB**
- Simulirana razmera: **10.000** fajlova, period **12 meseci**
