using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinanceManager.ClientApp.Models;

namespace FinanceManager.ClientApp.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<decimal> GetTotalBalanceAsync();
        Task<Dictionary<string, decimal>> GetAccountBalancesAsync();
        Task<List<TransactionViewModel>> GetRecentTransactionsAsync(int count = 5);
        Task<TransactionSummaryViewModel> GetCurrentMonthSummaryAsync();
        Task<List<BudgetProgressViewModel>> GetActiveBudgetsAsync();
        Task<FinancialFlowViewModel> GetFinancialFlowAsync(int months = 6);
        Task<Dictionary<string, decimal>> GetIncomeExpenseByMonthAsync(int months = 6);
        Task<List<CategorySummary>> GetTopExpenseCategoriesAsync(DateTime? startDate = null, DateTime? endDate = null);
    }
}