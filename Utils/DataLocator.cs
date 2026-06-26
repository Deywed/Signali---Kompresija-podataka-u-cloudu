using System;
using System.IO;

namespace CompressionBenchmark.Utils
{
    /// <summary>
    /// Pomoćna klasa za robustno pronalaženje "Data" foldera, nezavisno od toga
    /// odakle se aplikacija pokreće (iz IDE-a, iz bin foldera, ili iz generisanog
    /// BenchmarkDotNet procesa). Penje se uz stablo direktorijuma dok ne nađe
    /// folder koji sadrži "Data" podfolder.
    /// </summary>
    public static class DataLocator
    {
        public static string FindProjectRoot()
        {
            string dir = AppContext.BaseDirectory;
            while (!Directory.Exists(Path.Combine(dir, "Data")))
            {
                var parent = Directory.GetParent(dir);
                if (parent == null)
                {
                    throw new DirectoryNotFoundException(
                        "Nije pronađen 'Data' folder ni u jednom roditeljskom direktorijumu.");
                }
                dir = parent.FullName;
            }
            return dir;
        }

        /// <summary>
        /// Vraća apsolutnu putanju do fajla unutar Data foldera.
        /// Prihvata kako "dickens" tako i "Data/dickens".
        /// </summary>
        public static string ResolveDataFile(string fileName)
        {
            string relative = fileName.Replace('\\', '/');
            if (relative.StartsWith("Data/", StringComparison.OrdinalIgnoreCase))
            {
                relative = relative.Substring("Data/".Length);
            }
            return Path.Combine(FindProjectRoot(), "Data", relative);
        }
    }
}
