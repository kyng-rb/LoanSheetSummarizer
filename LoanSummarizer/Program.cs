using ClosedXML.Excel;
using LoanSummarizer.Common;
using LoanSummarizer.Extensions;
using LoanSummarizer.Services.GlobalSummary;


// Open the Excel file
using var workbook = new XLWorkbook(Constants.FilePath);
var table = FileSummary.Get(workbook);

LogService.Summary(workbook);
table.Dump();