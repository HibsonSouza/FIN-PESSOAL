using FinanceManager.ClientApp.Models;

namespace FinanceManager.ClientApp.Services.Interfaces
{
    public interface IReportService
    {
        Task<FinancialSummaryModel> GetFinancialSummary(DateTimeRange dateRange);
        Task<CategoryBreakdownModel> GetCategoryBreakdown(DateTimeRange dateRange, TransactionType transactionType);
        Task<MonthlyFinancialDataModel> GetMonthlyFinancialData(int year);
        Task<CashFlowReportModel> GetCashFlowReport(DateTimeRange dateRange);
        Task<ChartData> GetExpensesByCategoryChart(DateTimeRange dateRange);
        Task<ChartData> GetIncomeByCategoryChart(DateTimeRange dateRange);
        Task<NetWorthReportModel> GetNetWorthReport(DateTimeRange dateRange);
        Task<AccountBalanceReportModel> GetAccountBalanceReport(string accountId, DateTimeRange dateRange);
        Task<ExpenseTrendReportResult> GetExpensesTrend(DateTimeRange dateRange, string categoryId = null);
        Task<IncomeTrendReportResult> GetIncomeTrend(DateTimeRange dateRange, string categoryId = null);
        Task<SavingsRateReportResult> GetSavingsRateReport(DateTimeRange dateRange);
        Task<DashboardSummary> GetDashboardSummary();
    }
}