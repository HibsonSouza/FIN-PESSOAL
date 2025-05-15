using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinanceManager.ClientApp.Models;

namespace FinanceManager.ClientApp.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<decimal> GetTotalBalance();
        Task<decimal> GetMonthlyIncome();
        Task<decimal> GetMonthlyExpenses();
        Task<IEnumerable<ChartData>> GetCategoryExpenses(DateTimeRange dateRange);
        Task<IEnumerable<ChartData>> GetMonthlyBalanceHistory(int numberOfMonths = 6);
        Task<IEnumerable<ChartData>> GetSavingsGoalsProgress();
    }
}