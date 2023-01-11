using System.Data;
using ClosedXML.Excel;

namespace LoanSummarizer.Services.GlobalSummary;

public static class FileSummary
{
    public static DataTable Get(IXLWorkbook xlWorkbook)
    {

        //Init Table
        var table = InitializeDataTable();

        // Iterate through the sheets
        foreach (var worksheet in xlWorkbook.Worksheets)
        {
            FillRow(worksheet);
        }

        DataTable InitializeDataTable()
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("Sheet Name");
            dataTable.Columns.Add("Last Used Row");
            dataTable.Columns.Add("Last Used Column");
            dataTable.Columns.Add("Sheet Visibility");
            return dataTable;
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

        return table;
    }
}