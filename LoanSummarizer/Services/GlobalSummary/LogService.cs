using ClosedXML.Excel;
using LoanSummarizer.Common;

namespace LoanSummarizer.Services.GlobalSummary;

public static class LogService
{
    public static void Summary(IXLWorkbook xlWorkbook)
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
}