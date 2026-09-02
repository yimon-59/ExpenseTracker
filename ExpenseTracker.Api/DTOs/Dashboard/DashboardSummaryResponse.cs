using ExpenseTracker.Api.Models;

namespace ExpenseTracker.Api.DTOs.Dashboard;

public class DashboardSummaryResponse
{
    public decimal TotalIncome { get; set; }

    public decimal TotalExpense { get; set; }

    public decimal Balance { get; set; }

    public decimal ThisMonthExpense { get; set; }

    public ExpenseCategory? TopExpenseCategory { get; set; }
}