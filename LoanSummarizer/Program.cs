using LoanSummarizer.Common;
using LoanSummarizer.Services.Management;


var manager = new ApplicationManager(Constants.FilePath);
manager.PrintSummaryGroupedByDate();
//manager.PrintSummary();