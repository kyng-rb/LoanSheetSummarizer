namespace LoanSummarizer.Services.Data;

public class LoanEvent
{
    public DateOnly Date { get; set; }
    public string Type { get; set; }
    public decimal PrincipalBeginning { get; set; }
    public decimal PrincipalCollected { get; set; }
    public decimal PrincipalEnding { get; set; }
    public decimal InterestBeginning { get; set; }
    public decimal InterestCollected { get; set; }
    public decimal InterestEnding { get; set; }
    public decimal CollectedAmount { get; set; }
    public string SheetName { get; set; }
}