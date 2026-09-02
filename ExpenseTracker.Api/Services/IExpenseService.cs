using ExpenseTracker.Api.DTOs.Common;
using ExpenseTracker.Api.DTOs.Expense;

namespace ExpenseTracker.Api.Services
{
    public interface IExpenseService
    {
        Task<PagedResponse<ExpenseResponse>> GetPagedAsync(
        ExpenseQuery query);

        Task<ExpenseResponse?> GetByIdAsync(int id);

        Task<ExpenseResponse> CreateAsync(CreateExpenseRequest request);

        Task<bool> UpdateAsync(int id, UpdateExpenseRequest request);

        Task<bool> DeleteAsync(int id);
    }
}
