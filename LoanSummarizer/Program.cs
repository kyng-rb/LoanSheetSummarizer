using System.Data;
using ClosedXML.Excel;
using LoanSummarizer.Common;

//Init Table
var table = InitializeDataTable();
// Open the Excel file
using var workbook = new XLWorkbook(Constants.FilePath);

// Iterate through the sheets
foreach (var worksheet in workbook.Worksheets)
{
    FillRow(worksheet);
}

LogSummary(workbook);
PrintDataTable(table);

DataTable InitializeDataTable()
{
    var dataTable = new DataTable();
    dataTable.Columns.Add("Sheet Name");
    dataTable.Columns.Add("Last Used Row");
    dataTable.Columns.Add("Last Used Column");
    dataTable.Columns.Add("Sheet Visibility");
    return dataTable;
}

void LogSummary(IXLWorkbook xlWorkbook)
{
    Console.WriteLine("-------------------------------Start-Summary----------------------------------");

    var loanInfoSheets = xlWorkbook.Worksheets
        .Where(x => !Constants.ExcludedSheets.Contains(x.Name))
        .ToArray();

    var totalSheetCount = loanInfoSheets.Length;
    var hiddenSheets = loanInfoSheets.Count(x => x.Visibility == XLWorksheetVisibility.Hidden);

    Console.WriteLine($"{totalSheetCount} sheets in total");
    Console.WriteLine($"{totalSheetCount - hiddenSheets} actives sheets");
    Console.WriteLine($"{hiddenSheets} hidden sheets");
    Console.WriteLine("-------------------------------End-Summary----------------------------------");
}

void FillRow(IXLWorksheet worksheet)
{
    // Get the last used column and row
    var lastColumn = worksheet.LastColumnUsed();
    var lastRow = worksheet.LastRowUsed();

    // Add a row to the DataTable
    var dataRow = table.NewRow();
    dataRow["Sheet Name"] = worksheet.Name;
    dataRow["Last Used Row"] = lastRow.RowNumber();
    dataRow["Last Used Column"] = lastColumn.ColumnNumber();
    dataRow["Sheet Visibility"] = worksheet.Visibility.ToString();
    table.Rows.Add(dataRow);
}

void PrintDataTable(DataTable dataTable)
{
    GenerateHorizontalLine();

    // Print the column names
    foreach (DataColumn column in dataTable.Columns)
    {
        Console.Write("| " + column.ColumnName.PadRight(20) + " ");
    }

    Console.WriteLine("|");

    GenerateHorizontalLine();
    
    // Print the data rows
    foreach (DataRow row in dataTable.Rows)
    {
        foreach (var item in row.ItemArray)
        {
            Console.Write("| " + item?.ToString()?.PadRight(20) + " ");
        }

        Console.WriteLine("|");
    }

    GenerateHorizontalLine();
}

void GenerateHorizontalLine()
{
    // Print the horizontal line delimiter
    Console.WriteLine(new string('-', table.Columns.Count * 23));
}