using ExpenseTracker.Api.Data;
using ExpenseTracker.Api.DTOs.Expense;
using ExpenseTracker.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Api.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly AppDbContext _context;

        public ExpenseRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Expense> CreateAsync(Expense expense)
        {
            _context.Expenses.Add(expense);

            await _context.SaveChangesAsync();

            return expense;
        }

        public async Task DeleteAsync(Expense expense)
        {
            _context.Expenses.Remove(expense);

            await _context.SaveChangesAsync();
        }

        public async Task<(List<Expense> Items, int TotalCount)> GetPagedAsync(
        int userId,
        ExpenseQuery query)
        {
            var expenses = _context.Expenses
                .Where(x => x.UserId == userId)
                .AsQueryable();

            // Search
            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                expenses = expenses.Where(x =>
                    x.Title.Contains(query.Search) ||
                    (x.Description != null &&
                     x.Description.Contains(query.Search)));
            }

            // Category
            if (!string.IsNullOrWhiteSpace(query.Category))
            {
                expenses = expenses.Where(x =>
                    x.Category == query.Category);
            }

            // From date
            if (query.FromDate.HasValue)
            {
                expenses = expenses.Where(x =>
                    x.ExpenseDate >= query.FromDate.Value);
            }

            // To date
            if (query.ToDate.HasValue)
            {
                expenses = expenses.Where(x =>
                    x.ExpenseDate <= query.ToDate.Value);
            }

            // Count before pagination
            var totalCount = await expenses.CountAsync();

            // Sorting
            expenses = query.SortBy.ToLower() switch
            {
                "amount" => query.Descending
                    ? expenses.OrderByDescending(x => x.Amount)
                    : expenses.OrderBy(x => x.Amount),

                "title" => query.Descending
                    ? expenses.OrderByDescending(x => x.Title)
                    : expenses.OrderBy(x => x.Title),

                _ => query.Descending
                    ? expenses.OrderByDescending(x => x.ExpenseDate)
                    : expenses.OrderBy(x => x.ExpenseDate)
            };

            // Pagination
            var items = await expenses
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        public async Task<Expense?> GetByIdAsync(int id, int userId)
        {
            return await _context.Expenses.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        }

        public async Task UpdateAsync(Expense expense)
        {
            await _context.SaveChangesAsync();
        }
    }
}
