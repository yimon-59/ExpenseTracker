using ExpenseTracker.Api.Models;

namespace ExpenseTracker.Api.DTOs.Expense
{
    public class CreateExpenseRequest
    {
        public string Title { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public ExpenseCategory Category { get; set; } 

        public DateTime ExpenseDate { get; set; }

        public string? Description { get; set; }
    }
}
