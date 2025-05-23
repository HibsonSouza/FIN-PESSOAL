using FinanceManager.Models;
using FinanceManager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinanceManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BudgetsController : ControllerBase
    {
        private readonly IBudgetService _budgetService;

        public BudgetsController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                return Unauthorized();
            }

            var budgets = await _budgetService.GetAllBudgetsAsync(userId);
            return Ok(budgets);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                return Unauthorized();
            }

            var budget = await _budgetService.GetBudgetByIdAsync(id);
            if (budget == null || budget.UserId != userId)
            {
                return NotFound();
            }

            return Ok(budget);
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActive()
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                return Unauthorized();
            }

            var budgets = await _budgetService.GetActiveBudgetsAsync(userId);
            return Ok(budgets);
        }

        [HttpGet("month")]
        public async Task<IActionResult> GetByMonth([FromQuery] int month, [FromQuery] int year)
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                return Unauthorized();
            }

            var budgets = await _budgetService.GetBudgetsByMonthAsync(userId, month, year);
            return Ok(budgets);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Budget budget)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                return Unauthorized();
            }

            // Verificar se já existe um orçamento para esta categoria neste mês
            var existingBudget = await _budgetService.GetBudgetByCategoryAndMonthAsync(
                budget.CategoryId, budget.Month, budget.Year);
                
            if (existingBudget != null)
            {
                return BadRequest("Já existe um orçamento para esta categoria neste mês");
            }

            budget.UserId = userId;
            var result = await _budgetService.CreateBudgetAsync(budget);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Budget budget)
        {
            if (id != budget.Id)
            {
                return BadRequest("ID na rota não corresponde ao ID no corpo da requisição");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                return Unauthorized();
            }

            var existingBudget = await _budgetService.GetBudgetByIdAsync(id);
            if (existingBudget == null || existingBudget.UserId != userId)
            {
                return NotFound();
            }

            budget.UserId = userId;
            var result = await _budgetService.UpdateBudgetAsync(budget);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                return Unauthorized();
            }

            var budget = await _budgetService.GetBudgetByIdAsync(id);
            if (budget == null || budget.UserId != userId)
            {
                return NotFound();
            }

            await _budgetService.DeleteBudgetAsync(id);

            return NoContent();
        }
    }
}
