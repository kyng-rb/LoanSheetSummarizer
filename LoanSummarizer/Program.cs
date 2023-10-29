using LoanSummarizer.Common;
using LoanSummarizer.Services.Management;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

var filePathConfig = configuration.GetSection("FilePath");
var manager = new ApplicationManager(filePathConfig.Value);
manager.PrintSummaryGroupedByDate(2023);
//manager.PrintSummary();