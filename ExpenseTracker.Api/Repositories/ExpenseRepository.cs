using ExpenseTracker.Api.Data;
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

        public async Task<List<Expense>> GetAllAsync(int userId)
        {
            return await _context.Expenses
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.ExpenseDate)
            .ToListAsync();
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
