using System;
using System.Collections.Generic;

namespace FinanceManager.ClientApp.Models
{
    public class DashboardViewModel
    {
        // Financial Summary
        public decimal TotalBalance { get; set; }
        public int ActiveAccountsCount { get; set; }
        public decimal MonthlyIncome { get; set; }
        public decimal MonthlyExpenses { get; set; }
        public decimal MonthlySavings => MonthlyIncome - MonthlyExpenses;
        public decimal MonthlySavingsPercentage => MonthlyIncome > 0 ? Math.Round(MonthlySavings / MonthlyIncome * 100, 1m) : 0;
        
        // Chart Data
        public IncomeVsExpensesChartData IncomeVsExpensesData { get; set; } = new IncomeVsExpensesChartData();
        public ExpensesByCategoryChartData ExpensesByCategoryData { get; set; } = new ExpensesByCategoryChartData();
        
        // Recent Transactions
        public List<TransactionViewModel> RecentTransactions { get; set; } = new List<TransactionViewModel>();
        
        // Accounts Summary
        public List<AccountSummaryViewModel> AccountsSummary { get; set; } = new List<AccountSummaryViewModel>();
        
        // Budget Progress
        public List<BudgetProgressViewModel> BudgetProgress { get; set; } = new List<BudgetProgressViewModel>();
        
        // Upcoming Bills
        public List<UpcomingBillViewModel> UpcomingBills { get; set; } = new List<UpcomingBillViewModel>();
    }

    public class IncomeVsExpensesChartData
    {
        public string[] Labels { get; set; } = new string[6];
        public decimal[] IncomeData { get; set; } = new decimal[6];
        public decimal[] ExpensesData { get; set; } = new decimal[6];
    }

    public class ExpensesByCategoryChartData
    {
        public string[] Categories { get; set; } = Array.Empty<string>();
        public decimal[] Amounts { get; set; } = Array.Empty<decimal>();
    }

    public class UpcomingBillViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsPaid { get; set; }
        public string Type { get; set; } // CreditCard, Recurring, etc.
        public string IconName { get; set; }
        public string Color { get; set; }
        public int DaysUntilDue => (int)(DueDate - DateTime.Today).TotalDays;
    }
}
