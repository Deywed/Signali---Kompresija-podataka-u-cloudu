using System;
using CompressionBenchmark.Models;

namespace CompressionBenchmark.Utils
{
    public static class CloudCostCalculator
    {
        // Standard AWS S3 pricing: ~$0.023 per GB per month
        private const double StorageCostPerGb = 0.023;

        // AWS Data Transfer Out (Internet) pricing: ~$0.09 per GB
        private const double TransferCostPerGb = 0.09;

        // 1024^3 bytes in a Gigabyte (1024 * 1024 * 1024)
        private const double BytesInGigabyte = 1073741824.0;

        public static CostEstimationResult EstimateCosts(long originalSizeBytes, long compressedSizeBytes, int months = 12)
        {
            // Convert raw bytes to Gigabytes (GB)
            double originalGb = originalSizeBytes / BytesInGigabyte;
            double compressedGb = compressedSizeBytes / BytesInGigabyte;

            // Storage cost calculation (GB * Price * Duration in months)
            double originalStorage = originalGb * StorageCostPerGb * months;
            double compressedStorage = compressedGb * StorageCostPerGb * months;

            // Network egress cost calculation (Single data transfer out)
            double originalTransfer = originalGb * TransferCostPerGb;
            double compressedTransfer = compressedGb * TransferCostPerGb;

            // Total financial savings
            double totalSavings = (originalStorage + originalTransfer) - (compressedStorage + compressedTransfer);

            return new CostEstimationResult(
                originalStorage,
                compressedStorage,
                originalTransfer,
                compressedTransfer,
                totalSavings
            );
        }
    }
}