using ExpenseTracker.Api.DTOs.Income;
using ExpenseTracker.Api.Models;
using ExpenseTracker.Api.Repositories;

namespace ExpenseTracker.Api.Services
{
    public class IncomeService : IIncomeService
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly ICurrentUserService _currentUserService;
        public IncomeService(IIncomeRepository incomeRepository, ICurrentUserService currentUserService)
        {
            _incomeRepository = incomeRepository;
            _currentUserService = currentUserService;
        }

        public async Task<IncomeResponse> CreateAsync(CreateIncomeRequest request)
        {
            var userId = _currentUserService.GetUserId();
            var income = new Income
            {
                Title = request.Title,
                Amount = request.Amount,
                Source = request.Source,
                IncomeDate = request.IncomeDate,
                Description = request.Description,
                CreatedAt = DateTime.UtcNow,
                UserId = userId
            };
            await _incomeRepository.CreateAsync(income);
            return MapToResponse(income);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var userId = _currentUserService.GetUserId();
            var income = await _incomeRepository.GetByIdAsync(id, userId);

            if (income == null)
                return false;

            await _incomeRepository.DeleteAsync(income);

            return true;
        }

        public async Task<List<IncomeResponse>> GetAllAsync()
        {
            var userId = _currentUserService.GetUserId();
            var incomes = await _incomeRepository.GetAllAsync(userId);
            return incomes.Select(MapToResponse).ToList();
        }

        public async Task<IncomeResponse?> GetByIdAsync(int id)
        {
            var userId = _currentUserService.GetUserId();
            var income = await _incomeRepository.GetByIdAsync(id, userId);
            return income != null ? MapToResponse(income) : null;
        }

        public async Task<bool> UpdateAsync(int id, UpdateIncomeRequest request)
        {
            var userId = _currentUserService.GetUserId();
            var income = await _incomeRepository.GetByIdAsync(id, userId);

            if (income == null)
                return false;

            income.Title = request.Title;
            income.Amount = request.Amount;
            income.Source = request.Source;
            income.IncomeDate = request.IncomeDate;
            income.Description = request.Description;

            await _incomeRepository.UpdateAsync(income);

            return true;
        }

        private static IncomeResponse MapToResponse(
       Income income)
        {
            return new IncomeResponse
            {
                Id = income.Id,
                Title = income.Title,
                Amount = income.Amount,
                Source = income.Source,
                IncomeDate = income.IncomeDate,
                Description = income.Description,
                CreatedAt = income.CreatedAt
            };
        }
    }
}
