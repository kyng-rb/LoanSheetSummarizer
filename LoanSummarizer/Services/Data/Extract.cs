using System.Data;
using ClosedXML.Excel;

namespace LoanSummarizer.Services.Data;

public class Extract
{
    public IEnumerable<LoanEvent> Execute(IXLWorksheet sheet)
    {
        var range = GetEventsRange(sheet);

        var loanEvents = new List<LoanEvent>();
        foreach (var row in range.Rows())
        {
            var cells = row.Cells();
            var loanEvent = new LoanEvent();
            loanEvent.Date = DateOnly.FromDateTime(DateTime.Parse(cells.ElementAt(0).Value.ToString()!));
            loanEvent.Type = cells.First().CellRight().Value.ToString()!;
            loanEvent.PrincipalBeginning = decimal.Parse(cells.ElementAt(2).Value.ToString()!);
            loanEvent.PrincipalCollected = decimal.Parse(cells.ElementAt(3).Value.ToString()!);
            loanEvent.PrincipalEnding = decimal.Parse(cells.ElementAt(4).Value.ToString()!);
            loanEvent.InterestBeginning = decimal.Parse(cells.ElementAt(5).Value.ToString()!);
            loanEvent.InterestCollected = decimal.Parse(cells.ElementAt(6).Value.ToString()!);
            loanEvent.InterestEnding = decimal.Parse(cells.ElementAt(7).Value.ToString()!);
            loanEvent.CollectedAmount = decimal.Parse(cells.ElementAt(8).Value.ToString()!);
            loanEvent.SheetName = sheet.Name;
            loanEvents.Add(loanEvent);
        }
        return loanEvents;
    }

    private IXLRange GetEventsRange(IXLWorksheet sheet)
    {
        const string topLeft = "A3";
        const string receivedAmountColumn = "I";
        
        var bottomRight = sheet.Column(receivedAmountColumn).LastCellUsed().Address.ToString();
        var range = sheet.Range($"{topLeft}:{bottomRight}");
        return range;
    }
}