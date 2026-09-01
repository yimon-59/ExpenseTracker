using ExpenseTracker.Api.Data;
using ExpenseTracker.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpensesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ExpensesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetExpenses()
    {
        var expenses = await _context.Expenses
            .OrderByDescending(x => x.ExpenseDate)
            .ToListAsync();

        return Ok(expenses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetExpense(int id)
    {
        var expense = await _context.Expenses
            .FirstOrDefaultAsync(x => x.Id == id);

        if (expense == null)
        {
            return NotFound();
        }

        return Ok(expense);
    }

    [HttpPost]
    public async Task<IActionResult> CreateExpense(Expense expense)
    {
        expense.CreatedAt = DateTime.UtcNow;

        _context.Expenses.Add(expense);

        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetExpense),
            new { id = expense.Id },
            expense);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExpense(
        int id,
        Expense request)
    {
        var expense = await _context.Expenses
            .FirstOrDefaultAsync(x => x.Id == id);

        if (expense == null)
        {
            return NotFound();
        }

        expense.Title = request.Title;
        expense.Amount = request.Amount;
        expense.Category = request.Category;
        expense.ExpenseDate = request.ExpenseDate;
        expense.Description = request.Description;

        await _context.SaveChangesAsync();

        return Ok(expense);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExpense(int id)
    {
        var expense = await _context.Expenses
            .FirstOrDefaultAsync(x => x.Id == id);

        if (expense == null)
        {
            return NotFound();
        }

        _context.Expenses.Remove(expense);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}