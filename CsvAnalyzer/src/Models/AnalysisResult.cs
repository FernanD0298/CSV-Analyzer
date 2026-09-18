namespace CsvAnalyzer.Models;

public record AnalysisResult
{
    public decimal TotalRevenue;
    public string? BestSellingProduct;
    public int TotalItems;
    public DateTime BestSellingDate;
    public decimal AverageSale;
}