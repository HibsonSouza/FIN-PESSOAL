using FinanceManager.ClientApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceManager.ClientApp.Services.Interfaces
{
    public interface IReportService
    {
        Task<IncomeExpenseReportResult> GetIncomeExpenseReportAsync(DateRange dateRange);
        Task<CategoryDistributionReportResult> GetCategoryDistributionReportAsync(DateRange dateRange, TransactionType type);
        Task<CashflowReportResult> GetCashflowReportAsync(DateRange dateRange);
        Task<NetWorthReportResult> GetNetWorthReportAsync(DateRange dateRange);
        Task<AccountBalanceHistoryReportResult> GetAccountBalanceHistoryReportAsync(string accountId, DateRange dateRange);
        Task<BudgetPerformanceReportResult> GetBudgetPerformanceReportAsync(DateRange dateRange);
        Task<List<TransactionReportItem>> GetTransactionReportAsync(TransactionReportFilter filter);
        Task<ExpensesTrendReportResult> GetExpensesTrendReportAsync(DateRange dateRange, string categoryId = null);
        Task<IncomeTrendReportResult> GetIncomeTrendReportAsync(DateRange dateRange, string categoryId = null);
        Task<SavingsRateReportResult> GetSavingsRateReportAsync(DateRange dateRange);
        Task<ChartData> GetReportChartDataAsync(ReportType reportType, DateRange dateRange, string additionalFilter = null);
        Task<byte[]> ExportReportAsync(ReportType reportType, DateRange dateRange, ReportExportFormat format);
    }

    public enum ReportType
    {
        IncomeExpense,
        CategoryDistribution,
        Cashflow,
        NetWorth,
        AccountBalanceHistory,
        BudgetPerformance,
        TransactionList,
        ExpensesTrend,
        IncomeTrend,
        SavingsRate
    }

    public enum ReportExportFormat
    {
        PDF,
        Excel,
        CSV,
        JSON
    }

    public class TransactionReportFilter
    {
        public DateRange DateRange { get; set; }
        public List<string> AccountIds { get; set; }
        public List<string> CategoryIds { get; set; }
        public List<TransactionType> TransactionTypes { get; set; }
        public decimal? MinAmount { get; set; }
        public decimal? MaxAmount { get; set; }
        public string SearchTerm { get; set; }
        public bool IncludePending { get; set; }
        public string SortBy { get; set; }
        public bool SortAscending { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 50;

        public TransactionReportFilter()
        {
            AccountIds = new List<string>();
            CategoryIds = new List<string>();
            TransactionTypes = new List<TransactionType>();
            DateRange = new DateRange();
            SortBy = "Date";
            SortAscending = false;
        }
    }

    public class TransactionReportItem
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public TransactionType Type { get; set; }
        public string Category { get; set; }
        public string CategoryId { get; set; }
        public string Account { get; set; }
        public string AccountId { get; set; }
        public bool IsPending { get; set; }
        public string Notes { get; set; }
        public List<string> Tags { get; set; }
    }

    public class IncomeExpenseReportResult
    {
        public decimal TotalIncome { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal NetCashflow { get; set; }
        public double SavingsRate { get; set; }
        public List<CategorySummary> TopIncomeCategories { get; set; }
        public List<CategorySummary> TopExpenseCategories { get; set; }
        public ChartData IncomeVsExpenseChart { get; set; }
        public ChartData MonthlyTrendChart { get; set; }
        public List<MonthlySummary> MonthlySummaries { get; set; }
    }

    public class CategoryDistributionReportResult
    {
        public TransactionType Type { get; set; }
        public decimal Total { get; set; }
        public List<CategorySummary> Categories { get; set; }
        public ChartData PieChart { get; set; }
        public ChartData TrendChart { get; set; }
    }

    public class CashflowReportResult
    {
        public decimal NetCashflow { get; set; }
        public List<MonthlySummary> MonthlyCashflow { get; set; }
        public ChartData CashflowChart { get; set; }
        public decimal AverageMonthlyIncome { get; set; }
        public decimal AverageMonthlyExpenses { get; set; }
        public decimal AverageNetCashflow { get; set; }
    }

    public class NetWorthReportResult
    {
        public decimal TotalAssets { get; set; }
        public decimal TotalLiabilities { get; set; }
        public decimal NetWorth { get; set; }
        public List<AccountBalance> Assets { get; set; }
        public List<AccountBalance> Liabilities { get; set; }
        public List<NetWorthHistory> History { get; set; }
        public ChartData NetWorthChart { get; set; }
        public decimal NetWorthChange { get; set; }
        public double NetWorthChangePercentage { get; set; }
    }

    public class AccountBalanceHistoryReportResult
    {
        public string AccountId { get; set; }
        public string AccountName { get; set; }
        public decimal CurrentBalance { get; set; }
        public decimal StartingBalance { get; set; }
        public decimal BalanceChange { get; set; }
        public double BalanceChangePercentage { get; set; }
        public List<BalanceHistory> History { get; set; }
        public ChartData BalanceChart { get; set; }
        public decimal AverageBalance { get; set; }
        public decimal HighestBalance { get; set; }
        public decimal LowestBalance { get; set; }
    }

    public class BudgetPerformanceReportResult
    {
        public decimal TotalBudgeted { get; set; }
        public decimal TotalSpent { get; set; }
        public decimal RemainingBudget { get; set; }
        public double OverallPerformance { get; set; }
        public List<BudgetCategorySummary> Categories { get; set; }
        public ChartData PerformanceChart { get; set; }
        public ChartData TrendChart { get; set; }
    }

    public class ExpensesTrendReportResult
    {
        public List<MonthlyCategoryAmount> MonthlyExpenses { get; set; }
        public ChartData TrendChart { get; set; }
        public decimal AverageMonthlyExpense { get; set; }
        public decimal HighestMonthlyExpense { get; set; }
        public decimal LowestMonthlyExpense { get; set; }
        public CategorySummary HighestExpenseCategory { get; set; }
    }

    public class IncomeTrendReportResult
    {
        public List<MonthlyCategoryAmount> MonthlyIncome { get; set; }
        public ChartData TrendChart { get; set; }
        public decimal AverageMonthlyIncome { get; set; }
        public decimal HighestMonthlyIncome { get; set; }
        public decimal LowestMonthlyIncome { get; set; }
        public CategorySummary HighestIncomeCategory { get; set; }
    }

    public class SavingsRateReportResult
    {
        public List<MonthlySavingsRate> MonthlySavingsRates { get; set; }
        public double AverageSavingsRate { get; set; }
        public double HighestSavingsRate { get; set; }
        public double LowestSavingsRate { get; set; }
        public ChartData SavingsRateChart { get; set; }
    }

    public class CategorySummary
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public double Percentage { get; set; }
        public string Color { get; set; }
        public TransactionType Type { get; set; }
    }

    public class MonthlySummary
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string MonthName { get; set; }
        public decimal Income { get; set; }
        public decimal Expenses { get; set; }
        public decimal NetCashflow { get; set; }
        public double SavingsRate { get; set; }
    }

    public class AccountBalance
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Balance { get; set; }
        public double Percentage { get; set; }
    }

    public class NetWorthHistory
    {
        public DateTime Date { get; set; }
        public decimal Assets { get; set; }
        public decimal Liabilities { get; set; }
        public decimal NetWorth { get; set; }
    }

    public class BalanceHistory
    {
        public DateTime Date { get; set; }
        public decimal Balance { get; set; }
        public decimal Change { get; set; }
    }

    public class BudgetCategorySummary
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Budgeted { get; set; }
        public decimal Spent { get; set; }
        public decimal Remaining { get; set; }
        public double PercentUsed { get; set; }
        public string Status { get; set; }
        public string Color { get; set; }
    }

    public class MonthlyCategoryAmount
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string MonthName { get; set; }
        public decimal TotalAmount { get; set; }
        public List<CategoryAmount> Categories { get; set; }
    }

    public class CategoryAmount
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public double Percentage { get; set; }
        public string Color { get; set; }
    }

    public class MonthlySavingsRate
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string MonthName { get; set; }
        public decimal Income { get; set; }
        public decimal Expenses { get; set; }
        public decimal Saved { get; set; }
        public double SavingsRate { get; set; }
    }
}