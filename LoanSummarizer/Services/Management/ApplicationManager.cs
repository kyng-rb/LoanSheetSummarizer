using System.Data;
using ClosedXML.Excel;
using LoanSummarizer.Common;
using LoanSummarizer.Services.Data;

namespace LoanSummarizer.Services.Management;

public partial class ApplicationManager
    : IApplicationManager
{
    private readonly string _filePath;
    private readonly IEnumerable<LoanEvent> _events;
    public ApplicationManager(string filePath)
    {
        this._filePath = filePath;
        _events = GetData();
    }

    private IEnumerable<LoanEvent> GetData()
    {
        using var workbook = new XLWorkbook(_filePath);
        var extract = new Extract();
        var events = new List<LoanEvent>();
        foreach (var sheet in workbook.Worksheets.Where(x => !Constants.ExcludedSheets.Contains(x.Name)))
        {
            events.AddRange(extract.Execute(sheet));
        }

        return events;
    }

    public void PrintEvents()
    {
        foreach (var row in _events)
        {
            Console.WriteLine("Name: " + row.SheetName);
            Console.WriteLine("Date: " + row.Date);
            Console.WriteLine("Type: " + row.Type);
            Console.WriteLine("PrincipalBeginning: " + row.PrincipalBeginning);
            Console.WriteLine("PrincipalCollected: " + row.PrincipalCollected);
            Console.WriteLine("PrincipalEnding: " + row.PrincipalEnding);
            Console.WriteLine("InterestBeginning: " + row.InterestBeginning);
            Console.WriteLine("InterestCollected: " + row.InterestCollected);
            Console.WriteLine("InterestEnding: " + row.InterestEnding);
            Console.WriteLine("CollectedAmount: " + row.CollectedAmount);
            Console.WriteLine("/////////////////////////////////");
        }
    }

    public void PrintTotalEarnedInterest()
    {
        throw new NotImplementedException();
    }

    public void PrintTotalPendingInterest()
    {
        throw new NotImplementedException();
    }

    public void PrintTotalPendingPrincipal()
    {
        throw new NotImplementedException();
    }
}