using CsvAnalyzer.Services;
using CsvAnalyzer.Models;

namespace CsvAnalyzer.Tests;

public class SalesAnalyzerTest
{
    [Fact]
    public void Analyze_SingleSales()
    {
        List<Sales> sales = new List<Sales>
        {
            new Sales {Date = new DateTime(2026, 09, 10), Product = "mouse", Quantity = 2, UnitPrice = 100}
        };
        
        var analyze = new SalesAnalyzer();
        AnalysisResult result = analyze.Analyze(sales);
        
        Assert.Equal(200, result.TotalRevenue);
        Assert.Equal(2, result.TotalItems);
        Assert.Equal(200, result.AverageSale);
        Assert.Equal("mouse", result.BestSellingProduct);
        Assert.Equal(new DateTime(2026, 09, 10), result.BestSellingDate);
    }
    
    [Fact]
    public void Analyze_MultipleSales()
    {
        DateTime date = new DateTime(2026, 09, 10);
        List<Sales> sales = new List<Sales>
        {
            new Sales {Date = date, Product = "mouse", Quantity = 2, UnitPrice = 100},
            new Sales {Date = new DateTime(2026, 09, 9), Product = "keyboard", Quantity = 3, UnitPrice = 200},
            new Sales {Date = new DateTime(2026, 09, 8), Product = "monitor", Quantity = 4, UnitPrice = 1000},
            new Sales {Date = date, Product = "mouse", Quantity = 50, UnitPrice = 100},
        };
        var analyze = new SalesAnalyzer();
        
        AnalysisResult result = analyze.Analyze(sales);
        
        Assert.Equal("mouse", result.BestSellingProduct);
        Assert.Equal(59, result.TotalItems);
        Assert.Equal(date, result.BestSellingDate);
    }

    [Fact]
    public void Analyze_NullList()
    {
        var analyze = new SalesAnalyzer();
        Assert.Throws<ArgumentNullException>(() => analyze.Analyze(null));
    }
    
    [Fact]
    public void  Analyze_EmptyList()
    {
        var analyze = new SalesAnalyzer();
        Assert.Throws<ArgumentException>(() => analyze.Analyze(new List<Sales>()));
    }
}