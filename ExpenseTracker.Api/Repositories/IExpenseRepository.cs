using ExpenseTracker.Api.Models;

namespace ExpenseTracker.Api.Repositories
{
    public interface IExpenseRepository
    {
        Task<List<Expense>> GetAllAsync();

        Task<Expense?> GetByIdAsync(int id);

        Task<Expense> CreateAsync(Expense expense);

        Task UpdateAsync(Expense expense);

        Task DeleteAsync(Expense expense);
    }
}
