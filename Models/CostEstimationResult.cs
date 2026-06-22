namespace CompressionBenchmark.Models
{
    public record CostEstimationResult(
        double OriginalStorageCost,
        double CompressedStorageCost,
        double OriginalTransferCost,
        double CompressedTransferCost,
        double TotalSavings
    );
}