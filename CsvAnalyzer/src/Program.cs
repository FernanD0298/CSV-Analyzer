using CsvAnalyzer.Models;
using CsvAnalyzer.Reports;
using CsvAnalyzer.Services;

namespace CsvAnalyzer;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: CsvAnalyzer <file-path.csv>");
            return;
        }
        
        try
        {
            CsvReader reader = new CsvReader();
            List<Sales> sales = reader.Read(args[0]);

            SalesAnalyzer analyzer = new SalesAnalyzer();
            AnalysisResult result = analyzer.Analyze(sales);

            ReportGenerator generator = new ReportGenerator();
            string report = generator.GenerateReport(result);
            Console.WriteLine(report);
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"File not found: {args[0]}");
        }
        catch (InvalidDataException ex)
        {
            Console.WriteLine($"The file CSV has a problem: {ex.Message}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"The file could not be analyzed: {ex.Message}");
        }
    }
}