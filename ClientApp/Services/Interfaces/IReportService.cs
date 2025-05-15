using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinanceManager.ClientApp.Models;

namespace FinanceManager.ClientApp.Services.Interfaces
{
    public interface IReportService
    {
        Task<List<CategorySummary>> GetExpensesByCategoryAsync(DateRange dateRange);
        Task<List<CategorySummary>> GetIncomeByCategoryAsync(DateRange dateRange);
        Task<Dictionary<string, decimal>> GetExpensesByMonthAsync(int year);
        Task<Dictionary<string, decimal>> GetIncomeByMonthAsync(int year);
        Task<Dictionary<string, Dictionary<string, decimal>>> GetCategoryExpensesByMonthAsync(int year, List<int> categoryIds = null);
        Task<Dictionary<string, Dictionary<string, decimal>>> GetCategoryIncomeByMonthAsync(int year, List<int> categoryIds = null);
        Task<List<MonthlyFinancialSummary>> GetYearlyFlowAsync(int year);
        Task<Dictionary<string, decimal>> GetExpensesByPaymentMethodAsync(DateRange dateRange);
        Task<byte[]> ExportReportAsync(string reportType, DateRange dateRange, string fileFormat);
    }
}