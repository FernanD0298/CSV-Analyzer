using CsvAnalyzer.Models;
using CsvAnalyzer.Reports;

namespace CsvAnalyzer.Tests;

public class ReportGeneratorTest
{
    [Fact]
    public void GenerateReport_ValidResult()
    {
        var result = new AnalysisResult
        {
            TotalRevenue = 5300,
            TotalItems = 14,
            BestSellingProduct = "mouse",
            BestSellingDate = new DateTime(2026, 09, 10),
            AverageSale = 1325
        };
        var generator = new ReportGenerator();
        string report = generator.GenerateReport(result);
        
        Assert.Contains("5300", report);
        Assert.Contains("mouse", report);
    }

    [Fact]
    public void GeneratorReport_NullResult()
    {
        var generator = new ReportGenerator();
        Assert.Throws<ArgumentNullException>(() => generator.GenerateReport(null));
    }
}