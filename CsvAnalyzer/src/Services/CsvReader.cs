using System.Globalization;
using CsvAnalyzer.Models;

namespace CsvAnalyzer.Services;

public class CsvReader
{
    
    public List<Sales> Read(string path)
    {
        List<Sales> sales = new List<Sales>();
        using StreamReader reader = new StreamReader(path);
        
        string? headerLine = reader.ReadLine();
        if (headerLine == null || !IsHeaderValid(headerLine.Split(',')))
            throw new InvalidDataException($"The file '{path}' it does not contain expected header (Date,Product,Quantity,UnitPrice)");
        
        int lineNumber = 1;
        while (!reader.EndOfStream)
        {
            lineNumber++;
            string? line = reader.ReadLine()!;
            string[] values =  line.Split(',');
            
            if (values.Length != 4)
                throw new InvalidDataException($"Line {lineNumber}: 4 columns were expected; {values.Length} were found ");

            try
            {
                sales.Add(new Sales
                {
                    Date = DateTime.ParseExact(values[0], "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    Product = values[1],
                    Quantity = int.Parse(values[2]),
                    UnitPrice = decimal.Parse(values[3], CultureInfo.InvariantCulture),
                });
            }
            catch (FormatException ex)
            {
                throw new InvalidDataException($"Line {lineNumber}: invalid data format ({ex.Message}).", ex);
            }
        }
        
        return sales;
    }

    private bool IsHeaderValid(string[] values)
    {
        return values.Length == 4
            && values[0] == "Date"
            && values[1] == "Product"
            && values[2] == "Quantity"
            && values[3] == "UnitPrice";
    }

}