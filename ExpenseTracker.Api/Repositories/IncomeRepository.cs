using ExpenseTracker.Api.Data;
using ExpenseTracker.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Api.Repositories
{
    public class IncomeRepository : IIncomeRepository
    {
        private readonly AppDbContext _context; 

        public IncomeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Income> CreateAsync(Income income)
        {
            _context.Incomes.Add(income);

            await _context.SaveChangesAsync();

            return income;
        }

        public async Task DeleteAsync(Income income)
        {
            _context.Incomes.Remove(income);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Income>> GetAllAsync(int userId)
        {
            return await _context.Incomes.Where(i => i.UserId == userId).ToListAsync();
        }

        public async Task<Income?> GetByIdAsync(int id, int userId)
        {
            return await _context.Incomes.FirstOrDefaultAsync(i => i.Id == id && i.UserId == userId);
        }

        public async Task UpdateAsync(Income income)
        {
            _context.Incomes.Update(income);

            await _context.SaveChangesAsync();
        }
    }
}
