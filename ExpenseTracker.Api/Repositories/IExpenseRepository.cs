using ExpenseTracker.Api.DTOs.Expense;
using ExpenseTracker.Api.Models;

namespace ExpenseTracker.Api.Repositories
{
    public interface IExpenseRepository
    {
        Task<(List<Expense> Items, int TotalCount)> GetPagedAsync(
        int userId, ExpenseQuery query);
        Task<Expense?> GetByIdAsync(int id, int userId);

        Task<Expense> CreateAsync(Expense expense);

        Task UpdateAsync(Expense expense);

        Task DeleteAsync(Expense expense);
    }
}
