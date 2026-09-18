namespace CsvAnalyzer.Models;

public record Sales
{
    public DateTime Date;
    public string? Product;
    public int Quantity;
    public decimal UnitPrice;
}