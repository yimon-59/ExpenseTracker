using ExpenseTracker.Api.DTOs.Common;
using ExpenseTracker.Api.DTOs.Expense;
using ExpenseTracker.Api.Models;
using ExpenseTracker.Api.Repositories;

namespace ExpenseTracker.Api.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _repository;
        private readonly ICurrentUserService _currentUserService;

        public ExpenseService(IExpenseRepository repository, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
        }

        public async Task<ExpenseResponse> CreateAsync(CreateExpenseRequest request)
        {
            var userId = _currentUserService.GetUserId();
            var expense = new Expense
            {
                Title = request.Title,
                Amount = request.Amount,
                Category = request.Category,
                ExpenseDate = request.ExpenseDate,
                Description = request.Description,
                CreatedAt = DateTime.UtcNow,
                UserId = userId
            };
            await _repository.CreateAsync(expense);    
            return MapToResponse(expense);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var userId = _currentUserService.GetUserId();
            var expense = await _repository.GetByIdAsync(id, userId);

            if (expense == null)
                return false;

            await _repository.DeleteAsync(expense);

            return true;
        }

        public async Task<PagedResponse<ExpenseResponse>> GetPagedAsync(
        ExpenseQuery query)
        {
            var userId = _currentUserService.GetUserId();

            // Protect API from unreasonable values
            if (query.Page < 1)
                query.Page = 1;

            if (query.PageSize < 1)
                query.PageSize = 10;

            if (query.PageSize > 100)
                query.PageSize = 100;

            var result = await _repository.GetPagedAsync(
                userId,
                query);

            return new PagedResponse<ExpenseResponse>
            {
                Items = result.Items
                    .Select(MapToResponse)
                    .ToList(),

                Page = query.Page,

                PageSize = query.PageSize,

                TotalCount = result.TotalCount
            };
        }

        public async Task<ExpenseResponse?> GetByIdAsync(int id)
        {
            var userId = _currentUserService.GetUserId();
            var expense = await _repository.GetByIdAsync(id, userId);

            return expense == null ? null : MapToResponse(expense);
        }

        public async Task<bool> UpdateAsync(int id, UpdateExpenseRequest request)
        {
            var userId = _currentUserService.GetUserId();
            var expense = await _repository.GetByIdAsync(id, userId);

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
