using FinanceManager.Models.Enums;
using FinanceManager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinanceManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("monthly-income-expense")]
        public async Task<IActionResult> GetMonthlyIncomeExpense([FromQuery] int year)
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                return Unauthorized();
            }

            var data = await _reportService.GetMonthlyIncomeExpenseAsync(userId, year);
            return Ok(data);
        }

        [HttpGet("category-breakdown")]
        public async Task<IActionResult> GetCategoryBreakdown(
            [FromQuery] TransactionType type,
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                return Unauthorized();
            }

            var data = await _reportService.GetCategoryBreakdownAsync(userId, type, startDate, endDate);
            return Ok(data);
        }

        [HttpGet("account-balances")]
        public async Task<IActionResult> GetAccountBalances()
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                return Unauthorized();
            }

            var data = await _reportService.GetAccountBalancesAsync(userId);
            return Ok(data);
        }

        [HttpGet("balance-progress")]
        public async Task<IActionResult> GetBalanceProgress(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                return Unauthorized();
            }

            var data = await _reportService.GetDailyBalanceProgressAsync(userId, startDate, endDate);
            return Ok(data);
        }

        [HttpGet("budget-vs-actual")]
        public async Task<IActionResult> GetBudgetVsActual(
            [FromQuery] int month,
            [FromQuery] int year)
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                return Unauthorized();
            }

            var data = await _reportService.GetBudgetVsActualAsync(userId, month, year);
            return Ok(data);
        }
    }
}
