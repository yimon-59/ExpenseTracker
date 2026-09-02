using ExpenseTracker.Api.DTOs.Income;

namespace ExpenseTracker.Api.Services;

public interface IIncomeService
{
    Task<List<IncomeResponse>> GetAllAsync();

    Task<IncomeResponse?> GetByIdAsync(int id);

    Task<IncomeResponse> CreateAsync(
        CreateIncomeRequest request);

    Task<bool> UpdateAsync(
        int id,
        UpdateIncomeRequest request);

    Task<bool> DeleteAsync(int id);
}