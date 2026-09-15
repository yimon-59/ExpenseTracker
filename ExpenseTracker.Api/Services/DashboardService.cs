using ExpenseTracker.Api.Data;
using ExpenseTracker.Api.DTOs.Dashboard;
using ExpenseTracker.Api.Helpers;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Api.Services;

public class DashboardService : IDashboardService
{
    private readonly AppDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IRedisCacheService _cache;
    public DashboardService(
        AppDbContext context,
        ICurrentUserService currentUserService,
        IRedisCacheService cache)
    {
        _context = context;
        _currentUserService = currentUserService;
        _cache = cache;
    }

    public async Task<DashboardSummaryResponse> GetSummaryAsync()
    {
        var userId = _currentUserService.GetUserId();
        var cacheKey = CacheKeys.DashboardSummary(userId);

        var cached = await _cache.GetAsync<DashboardSummaryResponse>(
            cacheKey);

        if (cached is not null)
        {
            return cached;
        }
        var totalIncome = await _context.Incomes
            .Where(x => x.UserId == userId)
            .SumAsync(x => (decimal?)x.Amount) ?? 0;

        var totalExpense = await _context.Expenses
            .Where(x => x.UserId == userId)
            .SumAsync(x => (decimal?)x.Amount) ?? 0;

        var now = DateTime.UtcNow;

        var monthStart = new DateTime(
            now.Year,
            now.Month,
            1);

        var nextMonth = monthStart.AddMonths(1);

        var thisMonthExpense = await _context.Expenses
            .Where(x =>
                x.UserId == userId &&
                x.ExpenseDate >= monthStart &&
                x.ExpenseDate < nextMonth)
            .SumAsync(x => (decimal?)x.Amount) ?? 0;

        var topCategory = await _context.Expenses
         .Where(x => x.UserId == userId)
         .GroupBy(x => x.Category)
         .Select(g => new
         {
             Category = g.Key,
             Total = g.Sum(x => x.Amount)
         })
         .OrderByDescending(x => x.Total)
         .FirstOrDefaultAsync();

        var result =  new DashboardSummaryResponse
        {
            TotalIncome = totalIncome,
            TotalExpense = totalExpense,
            Balance = totalIncome - totalExpense,
            ThisMonthExpense = thisMonthExpense,
            TopExpenseCategory = topCategory?.Category,
        };

        await _cache.SetAsync(
        cacheKey,
        result,
        TimeSpan.FromMinutes(5));

        return result;
    }
}