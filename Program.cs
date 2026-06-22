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
        static void Main(string[] args)
        {
            // 1. POKRETANJE BENCHMARK-A (Ovo ostaje za merenje CPU i RAM-a)
            Console.WriteLine("Pokretanje BenchmarkDotNet-a...");
            BenchmarkRunner.Run<CompressionPerformanceBenchmark>();

            Console.WriteLine("\n--- Benchmark završen. Pokretanje finansijske analize... ---\n");

            // 2. FINANSIJSKA ANALIZA (Čitanje fajla i proračun ušteda)
            // Koristimo istu logiku za pronalaženje Data foldera kao u Setup-u
            string currentDir = AppDomain.CurrentDomain.BaseDirectory;
            while (!Directory.Exists(Path.Combine(currentDir, "Data")))
            {
                var parent = Directory.GetParent(currentDir);
                if (parent == null) break;
                currentDir = parent.FullName;
            }

            string filePath = Path.Combine(currentDir, "Data", "dickens");
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Greška: Fajl {filePath} nije pronađen za finansijsku analizu.");
                return;
            }

            byte[] originalBytes = File.ReadAllBytes(filePath);
            long totalOriginalBytes = originalBytes.Length * 10000L; // Simulacija 10,000 transfera/fajlova

            // Lista svih strategija kroz koje ćemo proći petljom
            var strategies = new List<ICompressionStrategy>
            {
                new GzipStrategy(),
                new ZstdStrategy(),
                new Lz4Strategy(),
                new SnappyStrategy(),
                new BrotliStrategy()
            };

            // StringBuilder nam služi da sakupimo sav tekst pre upisa u fajl
            StringBuilder reportBuilder = new StringBuilder();

            // Zaglavlje izveštaja
            reportBuilder.AppendLine("=============================================================================");
            reportBuilder.AppendLine($"        CLOUD COST ESTIMATION REPORT - SILENCIA CORPUS (samba)              ");
            reportBuilder.AppendLine($"        Simulated Scale: 10,000 Files/Transfers | Duration: 12 Months       ");
            reportBuilder.AppendLine("=============================================================================");
            reportBuilder.AppendLine();

            // Prolazimo kroz svaki algoritam, kompresujemo podatak i računamo troškove
            foreach (var strategy in strategies)
            {
                try
                {
                    byte[] compressedBytes = strategy.Compress(originalBytes);
                    long totalCompressedBytes = compressedBytes.Length * 10000L;

                    CostEstimationResult estimation = CloudCostCalculator.EstimateCosts(totalOriginalBytes, totalCompressedBytes, months: 12);

                    // Formiranje teksta za trenutni algoritam
                    string section =
                        $"ALGORITHM: {strategy.Name}\n" +
                        $"-------------------------------------------------\n" +
                        $"  Original Size (Scale):    {(totalOriginalBytes / 1073741824.0):F2} GB\n" +
                        $"  Compressed Size (Scale):  {(totalCompressedBytes / 1073741824.0):F2} GB\n" +
                        $"  Original Storage Cost:    ${estimation.OriginalStorageCost:F2}\n" +
                        $"  Compressed Storage Cost:  ${estimation.CompressedStorageCost:F2}\n" +
                        $"  Original Transfer Cost:   ${estimation.OriginalTransferCost:F2}\n" +
                        $"  Compressed Transfer Cost: ${estimation.CompressedTransferCost:F2}\n" +
                        $"  -----------------------------------------------\n" +
                        $"  TOTAL ESTIMATED SAVINGS:  ${estimation.TotalSavings:F2}\n" +
                        $"=============================================================================\n";

                    reportBuilder.AppendLine(section);
                }
                catch (Exception ex)
                {
                    reportBuilder.AppendLine($"Greška pri obradi algoritma {strategy.Name}: {ex.Message}\n");
                }
            }

            // 3. ISPIS NA EKRAN (Konzola)
            string finalReportText = reportBuilder.ToString();
            Console.WriteLine(finalReportText);

            // 4. UPIS U FAJL
            // Fajl će se sačuvati u root folderu tvog projekta pod nazivom "cloud_cost_report.txt"
            string reportPath = Path.Combine(currentDir, "cloud_cost_report.txt");

            File.WriteAllText(reportPath, finalReportText);

            Console.WriteLine($"Izveštaj uspešno sačuvan na lokaciji: {reportPath}");
        }
    }
}