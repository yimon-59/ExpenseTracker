namespace ExpenseTracker.Api.DTOs.Income;

public class CreateIncomeRequest
{
    public string Title { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public string Source { get; set; } = string.Empty;

    public DateTime IncomeDate { get; set; }

    public string? Description { get; set; }
}