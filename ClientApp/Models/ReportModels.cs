using System.ComponentModel.DataAnnotations;

namespace FinanceManager.ClientApp.Models
{
    public enum ReportType
    {
        FinancialSummary,
        CategoryBreakdown,
        MonthlySpending,
        CashFlow,
        NetWorth,
        Savings,
        IncomeTrend,
        ExpenseTrend,
        BudgetPerformance
    }
    
    public class ReportRequestModel
    {
        public ReportType ReportType { get; set; }
        public DateTimeRange DateRange { get; set; } = new DateTimeRange();
        public TransactionType? TransactionType { get; set; }
        public string? CategoryId { get; set; }
        public int? MonthsCount { get; set; }
    }
    
    public class FinancialSummaryModel
    {
        public decimal TotalIncome { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal NetSavings { get; set; }
        public decimal SavingsRate { get; set; }
        public List<CategorySummaryModel> TopExpenseCategories { get; set; } = new();
        public List<CategorySummaryModel> TopIncomeCategories { get; set; } = new();
        public ChartData IncomeVsExpensesChart { get; set; } = new();
    }
    
    public class CategoryBreakdownModel
    {
        public TransactionType Type { get; set; }
        public List<CategorySummaryModel> Categories { get; set; } = new();
        public ChartData Chart { get; set; } = new();
    }
    
    public class CashFlowReportModel
    {
        public decimal TotalIncome { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal NetCashFlow { get; set; }
        public List<MonthlyCashFlow> MonthlyData { get; set; } = new();
        public ChartData CashFlowChart { get; set; } = new();
    }
    
    public class MonthlyCashFlow
    {
        public DateTime Month { get; set; }
        public decimal Income { get; set; }
        public decimal Expenses { get; set; }
        public decimal NetFlow { get; set; }
    }
    
    public class MonthlyFinancialDataModel
    {
        public List<MonthlySummary> MonthlyData { get; set; } = new();
        public ChartData IncomeChart { get; set; } = new();
        public ChartData ExpenseChart { get; set; } = new();
        public ChartData SavingsChart { get; set; } = new();
    }
    
    public class MonthlySummary
    {
        public DateTime Month { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal NetSavings { get; set; }
        public decimal SavingsRate { get; set; }
    }
    
    public class NetWorthReportModel
    {
        public decimal CurrentNetWorth { get; set; }
        public decimal MonthlyChange { get; set; }
        public decimal PercentChange { get; set; }
        public List<NetWorthHistory> History { get; set; } = new();
        public ChartData NetWorthChart { get; set; } = new();
    }
    
    public class NetWorthHistory
    {
        public DateTime Date { get; set; }
        public decimal Assets { get; set; }
        public decimal Liabilities { get; set; }
        public decimal NetWorth { get; set; }
    }
    
    public class AccountBalanceReportModel
    {
        public List<BalanceHistory> History { get; set; } = new();
        public ChartData BalanceChart { get; set; } = new();
    }
    
    public class BalanceHistory
    {
        public DateTime Date { get; set; }
        public string AccountId { get; set; } = string.Empty;
        public string AccountName { get; set; } = string.Empty;
        public decimal Balance { get; set; }
    }
    
    public class ExpenseTrendReportResult
    {
        public List<MonthlyExpenseByCategory> CategoryTrends { get; set; } = new();
        public ChartData TrendChart { get; set; } = new();
    }
    
    public class MonthlyExpenseByCategory
    {
        public DateTime Month { get; set; }
        public Dictionary<string, decimal> CategoryAmounts { get; set; } = new();
    }
    
    public class IncomeTrendReportResult
    {
        public List<MonthlyIncomeByCategory> CategoryTrends { get; set; } = new();
        public ChartData TrendChart { get; set; } = new();
    }
    
    public class MonthlyIncomeByCategory
    {
        public DateTime Month { get; set; }
        public Dictionary<string, decimal> CategoryAmounts { get; set; } = new();
    }
    
    public class SavingsRateReportResult
    {
        public List<MonthlySavingsRate> MonthlySavings { get; set; } = new();
        public ChartData SavingsChart { get; set; } = new();
        public decimal AverageSavingsRate { get; set; }
    }
    
    public class MonthlySavingsRate
    {
        public DateTime Month { get; set; }
        public decimal Income { get; set; }
        public decimal Expenses { get; set; }
        public decimal Savings { get; set; }
        public decimal SavingsRate { get; set; }
    }
    
    public class DashboardSummary
    {
        public decimal TotalBalance { get; set; }
        public decimal MonthlyIncome { get; set; }
        public decimal MonthlyExpenses { get; set; }
        public decimal MonthlySavings { get; set; }
        public decimal MonthlyBudget { get; set; }
        public decimal BudgetRemaining { get; set; }
        public decimal BudgetUsedPercentage { get; set; }
        public List<AccountViewModel> Accounts { get; set; } = new();
        public List<CategorySummaryModel> TopExpenseCategories { get; set; } = new();
        public List<TransactionViewModel> RecentTransactions { get; set; } = new();
        public List<BudgetProgressViewModel> BudgetProgress { get; set; } = new();
        public ChartData ExpensesByCategoryChart { get; set; } = new();
        public ChartData MonthlyTrendChart { get; set; } = new();
    }
}