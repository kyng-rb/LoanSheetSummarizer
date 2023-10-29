using System.Data;
using LoanSummarizer.Extensions;
using LoanSummarizer.Services.Data;

namespace LoanSummarizer.Services.Management;

public partial class ApplicationManager
{
    public void PrintSummaryGroupedByDate(int year)
    {
        var groupedRows = _events
            .GroupBy(row => new { row.Date.Year, row.Date.Month })
            .Where(w => w.Key.Year == year)
            .OrderBy(x => x.Key.Year)
            .ThenBy(s => s.Key.Month);

        var dataTable = new DataTable();
        dataTable.Columns.Add("Year", typeof(int));
        dataTable.Columns.Add("Month", typeof(int));
        dataTable.Columns.Add("Interest Collected", typeof(decimal));
        dataTable.Columns.Add("Principal Collected", typeof(decimal));
        dataTable.Columns.Add("Principal Borrowed", typeof(decimal));
        dataTable.Columns.Add("Amount Collected", typeof(decimal));

        foreach (var group in groupedRows)
        {
            var interestCollected = group.Sum(row => row.InterestCollected);
            var principalCollected = group.Sum(row => row.PrincipalCollected);
            var principalBorrowed = group.Where(x => x.Type.Equals("Loan", StringComparison.InvariantCultureIgnoreCase)).Sum(row => row.PrincipalBeginning);
            var amountCollected = interestCollected + principalCollected;

            dataTable.Rows.Add(group.Key.Year, group.Key.Month, interestCollected, principalCollected, principalBorrowed, amountCollected);
        }

        dataTable.Dump();
    }

    public void PrintSummary()
    {
        var groupedRows = _events
            .GroupBy(row => new { row.Date.Year, row.Date.Month })
            .OrderBy(x => x.Key.Year)
            .ThenBy(s => s.Key.Month);

        var interestCollectedAverage = groupedRows.Average(group => group.Sum(row => row.InterestCollected));
        var principalCollectedAverage = groupedRows.Average(group => group.Sum(row => row.PrincipalCollected));
        var totalInterestCollected = groupedRows.Sum(group => group.Sum(row => row.InterestCollected));
        var totalPrincipalCollected = groupedRows.Sum(group => group.Sum(row => row.PrincipalCollected));
        var maxInterestCollected = groupedRows.Max(group => group.Sum(row => row.InterestCollected));
        var maxPrincipalCollected = groupedRows.Max(group => group.Sum(row => row.PrincipalCollected));
        var maxInterestGroup = groupedRows.First(g => g.Sum(row => row.InterestCollected) == maxInterestCollected);
        var maxPrincipalGroup = groupedRows.First(g => g.Sum(row => row.PrincipalCollected) == maxPrincipalCollected);

        Console.WriteLine("Interest Collected Average: " + Math.Round(interestCollectedAverage));
        Console.WriteLine("Principal Collected Average: " + Math.Round(principalCollectedAverage));
        Console.WriteLine("Total Interest Collected: " + totalInterestCollected);
        Console.WriteLine("Total Principal Collected: " + totalPrincipalCollected);
        Console.WriteLine($"Max Interest Collected: {maxInterestCollected} in year: {maxInterestGroup.Key.Year} month: {maxInterestGroup.Key.Month}");
        Console.WriteLine($"Max Principal Collected: {maxPrincipalCollected} in year: {maxPrincipalGroup.Key.Year} month: {maxPrincipalGroup.Key.Month}");
    }
}