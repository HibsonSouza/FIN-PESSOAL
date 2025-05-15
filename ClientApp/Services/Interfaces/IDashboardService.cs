using FinanceManager.ClientApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceManager.ClientApp.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardViewModel> GetDashboardDataAsync();
        Task<DashboardViewModel> GetDashboardDataAsync(DateRange dateRange);
        Task<List<TransactionSummaryViewModel>> GetRecentTransactionsAsync(int count = 5);
        Task<decimal> GetTotalBalanceAsync();
        Task<ChartData> GetIncomeExpenseChartDataAsync(DateRange dateRange);
        Task<ChartData> GetCategoryDistributionChartDataAsync(DateRange dateRange, TransactionType type);
        Task<ChartData> GetAccountBalancesChartDataAsync();
    }

    public class DashboardViewModel
    {
        public decimal TotalBalance { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal NetCashflow { get; set; }
        public List<AccountBalanceViewModel> AccountBalances { get; set; }
        public List<TransactionSummaryViewModel> RecentTransactions { get; set; }
        public List<BudgetProgressViewModel> BudgetProgress { get; set; }
        public List<UpcomingBillViewModel> UpcomingBills { get; set; }
        public decimal PendingIncome { get; set; }
        public decimal PendingExpenses { get; set; }
        public int GoalsCount { get; set; }
        public int CompletedGoalsCount { get; set; }
        public ChartData IncomeExpenseChart { get; set; }
        public ChartData CategoryDistributionChart { get; set; }
        public ChartData CashflowTrendChart { get; set; }
        public List<FinancialAlertViewModel> Alerts { get; set; }
    }

    public class TransactionSummaryViewModel
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public TransactionType Type { get; set; }
        public string Category { get; set; }
        public string CategoryIcon { get; set; }
        public string CategoryColor { get; set; }
        public string Account { get; set; }
        public string AccountColor { get; set; }
    }

    public class AccountBalanceViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public string Icon { get; set; }
        public bool IsActive { get; set; }
    }

    public class BudgetProgressViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public decimal Spent { get; set; }
        public decimal Available { get; set; }
        public double PercentUsed { get; set; }
        public string Category { get; set; }
        public string CategoryIcon { get; set; }
        public string CategoryColor { get; set; }
        public BudgetPeriod Period { get; set; }
    }

    public class UpcomingBillViewModel
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsPaid { get; set; }
        public bool IsRecurring { get; set; }
        public string Category { get; set; }
        public string CategoryIcon { get; set; }
        public string CategoryColor { get; set; }
        public int DaysUntilDue { get; set; }
    }

    public class FinancialAlertViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public AlertType Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }
        public string ActionLink { get; set; }
        public string ActionText { get; set; }
    }

    public enum AlertType
    {
        Info,
        Warning,
        Success,
        Danger
    }

    public class ChartData
    {
        public string Title { get; set; }
        public List<string> Labels { get; set; }
        public List<ChartDataset> Datasets { get; set; }

        public ChartData()
        {
            Labels = new List<string>();
            Datasets = new List<ChartDataset>();
        }
    }

    public class ChartDataset
    {
        public string Label { get; set; }
        public List<decimal> Data { get; set; }
        public List<string> BackgroundColor { get; set; }
        public List<string> BorderColor { get; set; }
        public bool Fill { get; set; } = true;
        public int BorderWidth { get; set; } = 1;
        public string Type { get; set; }

        public ChartDataset()
        {
            Data = new List<decimal>();
            BackgroundColor = new List<string>();
            BorderColor = new List<string>();
        }

        public ChartDataset(string label) : this()
        {
            Label = label;
        }
    }
}