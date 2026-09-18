# CSV Sales Analyzer

console application that reads a sales CSV file, analyze the data, and prints a summary report - built as a portfolio project to practice separation of concerns and unit testing in C#

## Features

- Parses a CSV file with sales records (date, product, quantity, unit price)
- Calculates total revenue, items sold, average sale, best-selling product, and best-selling day
- Reports validation errors with clear messages (invalid header, malformed rows, missing file)
- Covered by unit tests (xUnit) for CSV parsing, analysis logic, and report generation

## CSV format

The input file must be comma-separated with the following header:

```
Date,Product,Quantity,UnitPrice
2026-09-08,Mouse,2,650
2026-09-09,Keyboard,1,900
```

- `Date` - format `yyyy-MM-dd`
- `Quantity` - whole number
- `UnitPrice` - decimal number

## Requirements

- [.NET 9 SDK](https://dotnet.microsoft.com/download)

## Running the project

```bash
dotnet run --project CsvAnalyzer -- path/to/sales.csv
```

Example output:

```
===============================
	Sales Report
===============================

Total revenue: 	5300
Items sold:    	14
Best product:  	Mouse
Best sale day: 	9/8/2026
Average sale:  	1325

===============================
```

## Running the tests

```bash
dotnet test
```

## Project structure

```
CsvAnalyzer/
|- Models/      #Sale, AnalysisResult
|- Services/    #CsvReader, SalesAnalyzer
|- Reports/     #ReportGenerator
|_ Program.cs   #Entry point, error handling

CvsAnalyzer.Test/   #Unit tests (xUnit)
```

## Design notes

Responsibilities are split into small, single-purpose classes instead of a single monolithic parser:

- **CsvReader** - reads and validates the raw file, builds `Sales` objects
- **SalesAnalyzer** - pure computation over in-memory data, no I/O
- **ReportGenerator** - formats results into a string; caller decides what to do with it

This separation is what makes each piece testable in isolation without mocking files or the console.