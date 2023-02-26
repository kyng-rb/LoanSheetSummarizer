using LoanSummarizer.Services.Data;

namespace LoanSummarizer.Services.Management;

public interface IApplicationManager
{
    void PrintEvents();

    void PrintSummaryGroupedByDate();
    
    void PrintTotalPendingInterest();
    
    void PrintTotalPendingPrincipal();

    void PrintSummary();
}