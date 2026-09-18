using CsvAnalyzer.Models;

namespace CsvAnalyzer.Reports;

public class ReportGenerator
{
    public string GenerateReport(AnalysisResult result)
    {
        if (result == null)
            throw new ArgumentNullException(nameof(result));
        
        var report = new System.Text.StringBuilder();
        report.AppendLine("===============================");
        report.AppendLine("\tSales Report");
        report.AppendLine("===============================\n");
        report.AppendLine("Total revenue: \t" + result.TotalRevenue);
        report.AppendLine("Items sold:    \t" + result.TotalItems);
        report.AppendLine("Best product:  \t" + result.BestSellingProduct);
        report.AppendLine("Best sale day: \t" + result.BestSellingDate);
        report.AppendLine("Average sale:  \t" + result.AverageSale);
        report.AppendLine();
        report.AppendLine("===============================\n");
        
        return  report.ToString();
    }
}