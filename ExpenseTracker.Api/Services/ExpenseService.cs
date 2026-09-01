using ExpenseTracker.Api.DTOs.Expense;
using ExpenseTracker.Api.Models;
using ExpenseTracker.Api.Repositories;

namespace ExpenseTracker.Api.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _repository;

        public ExpenseService(IExpenseRepository repository)
        {
            _repository = repository;
        }

        public async Task<ExpenseResponse> CreateAsync(CreateExpenseRequest request)
        {
            var expense = new Expense
            {
                Title = request.Title,
                Amount = request.Amount,
                Category = request.Category,
                ExpenseDate = request.ExpenseDate,
                Description = request.Description,
                CreatedAt = DateTime.UtcNow
            };
            await _repository.CreateAsync(expense);    
            return MapToResponse(expense);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var expense = await _repository.GetByIdAsync(id);

            if (expense == null)
                return false;

            await _repository.DeleteAsync(expense);

            return true;
        }

        public async Task<List<ExpenseResponse>> GetAllAsync()
        {
            var expenses = await _repository.GetAllAsync();

            return expenses.Select(MapToResponse).ToList();
        }

        public async Task<ExpenseResponse?> GetByIdAsync(int id)
        {
            var expense = await _repository.GetByIdAsync(id);

            return expense == null ? null : MapToResponse(expense);
        }

        public async Task<bool> UpdateAsync(int id, UpdateExpenseRequest request)
        {
            var expense = await _repository.GetByIdAsync(id);

            if (expense == null)
                return false;

            expense.Title = request.Title;
            expense.Amount = request.Amount;
            expense.Category = request.Category;
            expense.ExpenseDate = request.ExpenseDate;
            expense.Description = request.Description;

            await _repository.UpdateAsync(expense);

            return true;
        }

        private static ExpenseResponse MapToResponse(Expense expense)
        {
            return new ExpenseResponse
            {
                Id = expense.Id,
                Title = expense.Title,
                Amount = expense.Amount,
                Category = expense.Category,
                ExpenseDate = expense.ExpenseDate,
                Description = expense.Description,
                CreatedAt = expense.CreatedAt
            };
        }
    }
}
