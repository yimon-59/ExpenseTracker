using ExpenseTracker.Api.Models;

namespace ExpenseTracker.Api.Repositories
{
    public interface IExpenseRepository
    {
        Task<List<Expense>> GetAllAsync(int userId);

        Task<Expense?> GetByIdAsync(int id, int userId);

        Task<Expense> CreateAsync(Expense expense);

        Task UpdateAsync(Expense expense);

        Task DeleteAsync(Expense expense);
    }
}
