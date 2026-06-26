using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using CompressionBenchmark.Abstractions;
using CompressionBenchmark.Strategies;
using CompressionBenchmark.Utils;
using CompressionBenchmark.Models;
using BenchmarkDotNet.Running;
using CompressionBenchmark.Benchmarks;

namespace CompressionBenchmark
{
    class Program
    {
        // Simulirana razmera za finansijsku procenu: pretpostavljamo da u cloudu
        // čuvamo/prenosimo 10.000 ovakvih fajlova.
        private const long ScaleFactor = 10_000L;
        private const double BytesInGigabyte = 1073741824.0;

        // Korpus koji analiziramo (isti fajlovi kao u benchmark-u).
        private static readonly string[] CorpusFiles = { "dickens", "nci", "reymont", "20mb.csv" };

        static void Main(string[] args)
        {
            // 1. POKRETANJE BENCHMARK-A (merenje vremena izvršavanja i alokacija memorije)
            Console.WriteLine("Pokretanje BenchmarkDotNet-a...");
            BenchmarkRunner.Run<CompressionPerformanceBenchmark>();

            Console.WriteLine("\n--- Benchmark završen. Pokretanje finansijske analize... ---\n");

            // 2. FINANSIJSKA ANALIZA (čitanje fajlova i proračun ušteda)
            string root;
            try
            {
                root = DataLocator.FindProjectRoot();
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine($"Greška: {ex.Message}");
                return;
            }

            var strategies = new List<ICompressionStrategy>
            {
                new GzipStrategy(),
                new ZstdStrategy(),
                new Lz4Strategy(),
                new SnappyStrategy(),
                new BrotliStrategy()
            };

            StringBuilder reportBuilder = new StringBuilder();

            reportBuilder.AppendLine("=============================================================================");
            reportBuilder.AppendLine("        CLOUD COST ESTIMATION REPORT - SILESIA CORPUS                        ");
            reportBuilder.AppendLine($"        Simulated Scale: {ScaleFactor:N0} Files/Transfers | Duration: 12 Months        ");
            reportBuilder.AppendLine("=============================================================================");
            reportBuilder.AppendLine();

            foreach (string corpusFile in CorpusFiles)
            {
                string filePath = Path.Combine(root, "Data", corpusFile);
                if (!File.Exists(filePath))
                {
                    reportBuilder.AppendLine($"Upozorenje: Fajl '{corpusFile}' nije pronađen, preskačem.\n");
                    continue;
                }

                byte[] originalBytes = File.ReadAllBytes(filePath);
                AppendCorpusReport(reportBuilder, corpusFile, originalBytes, strategies);
            }

            // 3. ISPIS NA EKRAN (Konzola)
            string finalReportText = reportBuilder.ToString();
            Console.WriteLine(finalReportText);

            // 4. UPIS U FAJL (u root folder projekta)
            string reportPath = Path.Combine(root, "cloud_cost_report.txt");
            File.WriteAllText(reportPath, finalReportText);

            Console.WriteLine($"Izveštaj uspešno sačuvan na lokaciji: {reportPath}");
        }

        private static void AppendCorpusReport(
            StringBuilder report,
            string corpusName,
            byte[] originalBytes,
            List<ICompressionStrategy> strategies)
        {
            long totalOriginalBytes = originalBytes.Length * ScaleFactor;

            report.AppendLine("=============================================================================");
            report.AppendLine($"  CORPUS: {corpusName}   (fajl: {originalBytes.Length / (1024.0 * 1024.0):F2} MB, razmera: {totalOriginalBytes / BytesInGigabyte:F2} GB)");
            report.AppendLine("=============================================================================");
            report.AppendLine($"  {"ALGORITHM",-22}{"RATIO",8}{"SAVED",9}{"STORAGE $",14}{"TRANSFER $",14}{"SAVINGS $",14}");
            report.AppendLine("  -------------------------------------------------------------------------");

            foreach (var strategy in strategies)
            {
                try
                {
                    byte[] compressedBytes = strategy.Compress(originalBytes);
                    long totalCompressedBytes = compressedBytes.Length * ScaleFactor;

                    double ratio = (double)originalBytes.Length / compressedBytes.Length;
                    double savedPct = (1.0 - (double)compressedBytes.Length / originalBytes.Length) * 100.0;

                    CostEstimationResult est = CloudCostCalculator.EstimateCosts(totalOriginalBytes, totalCompressedBytes, months: 12);

                    report.AppendLine(
                        $"  {strategy.Name,-22}{ratio,7:F2}x{savedPct,8:F1}%{est.CompressedStorageCost,14:F2}{est.CompressedTransferCost,14:F2}{est.TotalSavings,14:F2}");
                }
                catch (Exception ex)
                {
                    report.AppendLine($"  {strategy.Name,-22}  GREŠKA: {ex.Message}");
                }
            }

            report.AppendLine();
        }
    }
}
