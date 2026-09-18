using CsvAnalyzer.Models;

namespace CsvAnalyzer.Services;

public class SalesAnalyzer
{
    struct daySale
    {
        public int totalItem;
        public decimal totalRevenue;
    }
    
    public AnalysisResult Analyze(List<Sales> sales)
    {
        if (sales == null)
            throw new ArgumentNullException(nameof(sales));
        
        if (sales.Count == 0)
            throw new ArgumentException("At least one sale must be provided");
        
        AnalysisResult result = new AnalysisResult();
        Dictionary<DateTime, daySale> saleDays = new Dictionary<DateTime, daySale>();
        Dictionary<string, int> saleItems =  new Dictionary<string, int>();
        decimal averageSale = 0;

        foreach (var sale in sales)
        {
            if (saleItems.ContainsKey(sale.Product))
            {
                saleItems[sale.Product] += sale.Quantity;
            }
            else
            {
                saleItems.Add(sale.Product, sale.Quantity);
            }
            
            if (saleDays.ContainsKey(sale.Date))
            {
                int saleTotalItems = saleDays[sale.Date].totalItem;
                decimal saleRevenue = saleDays[sale.Date].totalRevenue;
                daySale newSale = new daySale();
                newSale.totalItem += saleTotalItems + sale.Quantity;
                newSale.totalRevenue += saleRevenue + (sale.UnitPrice * sale.Quantity);
                saleDays[sale.Date] = newSale;
            }
            else
            {
                daySale newSale = new daySale();
                newSale.totalItem = sale.Quantity;
                newSale.totalRevenue = sale.UnitPrice * sale.Quantity;
                saleDays.Add(sale.Date, newSale);
            }

            averageSale += sale.UnitPrice * sale.Quantity;
        }

        result.AverageSale = averageSale / sales.Count;
        
        int sellingItems = 0;
        foreach (var key in saleItems.Keys)
        {
            if (saleItems[key] > sellingItems)
            {
                result.BestSellingProduct = key;
                sellingItems = saleItems[key];
            }
        }

        decimal dayRevenue = 0;
        foreach (var key in saleDays.Keys)
        {
            if (saleDays[key].totalRevenue > dayRevenue)
            {
                result.BestSellingDate = key;
                dayRevenue = saleDays[key].totalRevenue;
            }
            
            result.TotalRevenue += saleDays[key].totalRevenue;
            result.TotalItems += saleDays[key].totalItem;
        }

        return result;
    }
}