namespace ExpenseTracker.Api.DTOs.Expense
{
    public class ExpenseResponse
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public string Category { get; set; } = string.Empty;

        public DateTime ExpenseDate { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
