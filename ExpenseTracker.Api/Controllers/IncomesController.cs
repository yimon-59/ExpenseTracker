using ExpenseTracker.Api.DTOs.Expense;
using ExpenseTracker.Api.DTOs.Income;
using ExpenseTracker.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class IncomesController : ControllerBase
    {
        private readonly IIncomeService _service;

        public IncomesController(IIncomeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetIncomes()
        {
            var incomes = await _service.GetAllAsync();

            return Ok(incomes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetIncome(int id)
        {
            var income = await _service.GetByIdAsync(id);

            if (income == null)
                return NotFound();

            return Ok(income);
        }

        [HttpPost]
        public async Task<IActionResult> CreateIncome(
            CreateIncomeRequest request)
        {
            var income = await _service.CreateAsync(request);

            return CreatedAtAction(
                nameof(GetIncome),
                new { id = income.Id },
                income);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIncome(
            int id,
            UpdateIncomeRequest request)
        {
            var updated = await _service.UpdateAsync(id, request);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncome(int id)
        {
            var deleted = await _service.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
