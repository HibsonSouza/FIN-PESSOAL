using FinanceManager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinanceManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboardData([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                return Unauthorized();
            }

            var dateRange = new DateRange
            {
                StartDate = startDate ?? DateTime.Now.AddDays(-30),
                EndDate = endDate ?? DateTime.Now
            };

            var data = await _dashboardService.GetDashboardDataAsync(userId, dateRange);
            return Ok(data);
        }

        [HttpGet("total-balance")]
        public async Task<IActionResult> GetTotalBalance()
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                return Unauthorized();
            }

            var balance = await _dashboardService.GetTotalBalanceAsync(userId);
            return Ok(balance);
        }

        [HttpGet("monthly-income")]
        public async Task<IActionResult> GetMonthlyIncome([FromQuery] DateTime? month)
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                return Unauthorized();
            }

            var date = month ?? DateTime.Now;
            var income = await _dashboardService.GetMonthlyIncomeAsync(userId, date);
            return Ok(income);
        }

        [HttpGet("monthly-expenses")]
        public async Task<IActionResult> GetMonthlyExpenses([FromQuery] DateTime? month)
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                return Unauthorized();
            }

            var date = month ?? DateTime.Now;
            var expenses = await _dashboardService.GetMonthlyExpensesAsync(userId, date);
            return Ok(expenses);
        }

        [HttpGet("expenses-by-category")]
        public async Task<IActionResult> GetExpensesByCategory([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                return Unauthorized();
            }

            var dateRange = new DateRange
            {
                StartDate = startDate ?? DateTime.Now.AddDays(-30),
                EndDate = endDate ?? DateTime.Now
            };

            var categoryExpenses = await _dashboardService.GetExpensesByCategoryAsync(userId, dateRange);
            return Ok(categoryExpenses);
        }

        [HttpGet("cash-flow")]
        public async Task<IActionResult> GetCashFlow([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                return Unauthorized();
            }

            var dateRange = new DateRange
            {
                StartDate = startDate ?? DateTime.Now.AddDays(-30),
                EndDate = endDate ?? DateTime.Now
            };

            var cashFlow = await _dashboardService.GetCashFlowAsync(userId, dateRange);
            return Ok(cashFlow);
        }

        [HttpGet("budget-progress")]
        public async Task<IActionResult> GetBudgetProgress()
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                return Unauthorized();
            }

            var budgetProgress = await _dashboardService.GetBudgetProgressAsync(userId);
            return Ok(budgetProgress);
        }

        [HttpGet("savings-goals-progress")]
        public async Task<IActionResult> GetSavingsGoalsProgress()
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                return Unauthorized();
            }

            var goalsProgress = await _dashboardService.GetSavingsGoalsProgressAsync(userId);
            return Ok(goalsProgress);
        }
    }
}
