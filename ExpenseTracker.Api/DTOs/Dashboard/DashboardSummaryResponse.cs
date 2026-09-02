namespace ExpenseTracker.Api.DTOs.Dashboard;

public class DashboardSummaryResponse
{
    public decimal TotalIncome { get; set; }

    public decimal TotalExpense { get; set; }

    public decimal Balance { get; set; }

    public decimal ThisMonthExpense { get; set; }

    public string? TopExpenseCategory { get; set; }
}