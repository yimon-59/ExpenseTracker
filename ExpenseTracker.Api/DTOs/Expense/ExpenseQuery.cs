namespace ExpenseTracker.Api.DTOs.Expense;

public class ExpenseQuery
{
    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 10;

    public string? Search { get; set; }

    public string? Category { get; set; }

    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public string SortBy { get; set; } = "date";

    public bool Descending { get; set; } = true;
}