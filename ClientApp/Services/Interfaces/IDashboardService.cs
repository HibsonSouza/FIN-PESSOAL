using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinanceManager.ClientApp.Models;
using MudBlazor;

namespace FinanceManager.ClientApp.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardViewModel?> GetDashboardDataAsync(FinanceManager.ClientApp.Models.DateTimeRange dateRange);
        Task<decimal> GetTotalBalanceAsync(); 
        Task<decimal> GetMonthlyIncomeAsync(); 
        Task<decimal> GetMonthlyExpensesAsync(); 
        Task<IEnumerable<CategorySummaryViewModel>> GetExpensesByCategoryAsync(FinanceManager.ClientApp.Models.DateTimeRange dateRange); 
        Task<IEnumerable<BalanceForecastViewModel>> GetCashFlowAsync(FinanceManager.ClientApp.Models.DateTimeRange dateRange); 
        Task<IEnumerable<SavingsGoalProgress>> GetSavingsGoalsProgressAsync(); // Corrigido para SavingsGoalProgress
        Task<List<BudgetProgressViewModel>> GetBudgetProgressAsync(); 
    }
}