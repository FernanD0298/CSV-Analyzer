using System.Runtime.InteropServices.JavaScript;
using CsvAnalyzer.Services;

namespace CsvAnalyzer.Tests;

public class ReaderTest
{
    private static string CreateTempCsv(string content)
    {
        string path = Path.GetTempFileName();
        File.WriteAllText(path, content);
        return path;
    }
    
    [Fact]
    public void Read_ValidCsv_Test()
    {
        string tempPath = CreateTempCsv(
            "Date,Product,Quantity,UnitPrice\n" +
            "2026-09-08,mouse,2,560\n");

        try
        {
            var reader = new CsvReader();
            var sales = reader.Read(tempPath);

            Assert.Single(sales);
            Assert.Equal("mouse", sales[0].Product);
            Assert.Equal(2, sales[0].Quantity);
            Assert.Equal(560, sales[0].UnitPrice);
        }
        finally
        {
            File.Delete(tempPath);
        }
    }

    [Fact]
    public void Read_InvalidHeader_Test()
    {
        string path = CreateTempCsv(
            "Fecha,Producto,Cantidad,Precio\n" +
            "2026-09-08,mouse,2,560\n");

        try
        {
            var reader = new CsvReader();
            Assert.Throws<InvalidDataException>(() => reader.Read(path));
        }
        finally
        {
            File.Delete(path);
        }
    }

    [Fact]
    public void Read_RowWithWrongColumCount_Test()
    {
        string tempPath = CreateTempCsv(
            "Date,Product,Quantity,UnitPrice\n" +
            "2026-09-08,mouse,2\n");
        
        try
        {
            var reader = new CsvReader();
            Assert.Throws<InvalidDataException>(() => reader.Read(tempPath));
        }
        finally
        {
            File.Delete(tempPath);
        }
    }

    [Fact]
    public void Read_RowWith_InvalidNumber_Test()
    {
        string tempPath = CreateTempCsv(
            "Date,Product,Quantity,UnitPrice\n" +
            "2026-09-08,mouse,dos,560\n");
        
        try
        {
            var reader = new CsvReader();
            Assert.Throws<InvalidDataException>(() => reader.Read(tempPath));
        }
        finally
        {
            File.Delete(tempPath);
        }
    }

    [Fact]
    public void Read_FileDoesNotExist_Test()
    {
        var reader = new CsvReader();
        Assert.Throws<FileNotFoundException>(() => reader.Read("NotExist.csv"));
    }
}