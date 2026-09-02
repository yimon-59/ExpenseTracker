using ExpenseTracker.Api.Models;

namespace ExpenseTracker.Api.Repositories;

public interface IIncomeRepository
{
    Task<List<Income>> GetAllAsync(int userId);

    Task<Income?> GetByIdAsync(int id, int userId);

    Task<Income> CreateAsync(Income income);

    Task UpdateAsync(Income income);

    Task DeleteAsync(Income income);
}