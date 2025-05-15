using System;
using System.Collections.Generic;

namespace FinanceManager.ClientApp.Models
{
    public class InvestmentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public InvestmentType Type { get; set; }
        public InvestmentRiskLevel RiskLevel { get; set; }
        public decimal InitialValue { get; set; }
        public decimal CurrentValue { get; set; }
        public decimal Profitability { get; set; }  // Em percentual
        public decimal PerformancePercentage => (CurrentValue - InitialValue) / InitialValue * 100;
        public string Institution { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? MaturityDate { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; }
        public bool IsActive { get; set; }
        public List<InvestmentTransactionViewModel> Transactions { get; set; } = new List<InvestmentTransactionViewModel>();
    }

    public enum InvestmentType
    {
        SavingsAccount,
        FixedIncome,
        Stock,
        Fund,
        RealEstate,
        Cryptocurrency,
        Other
    }

    public enum InvestmentRiskLevel
    {
        VeryLow,
        Low,
        Medium,
        High,
        VeryHigh
    }

    public class InvestmentCreateModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public InvestmentType Type { get; set; }
        public InvestmentRiskLevel RiskLevel { get; set; }
        public decimal InitialValue { get; set; }
        public decimal CurrentValue { get; set; }
        public decimal Profitability { get; set; }
        public string Institution { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? MaturityDate { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; }
    }

    public class InvestmentUpdateModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public InvestmentType Type { get; set; }
        public InvestmentRiskLevel RiskLevel { get; set; }
        public decimal CurrentValue { get; set; }
        public decimal Profitability { get; set; }
        public string Institution { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? MaturityDate { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; }
        public bool IsActive { get; set; }
    }

    public class InvestmentTransactionViewModel
    {
        public int Id { get; set; }
        public int InvestmentId { get; set; }
        public string InvestmentName { get; set; }
        public DateTime Date { get; set; }
        public InvestmentTransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public string Description { get; set; }
        public decimal BalanceAfterTransaction { get; set; }
    }

    public enum InvestmentTransactionType
    {
        Deposit,
        Withdrawal,
        Income,
        Fee,
        Buy,
        Sell,
        Dividend,
        Interest,
        Split,
        Merger,
        Other
    }
    
    public static class InvestmentTransactionTypeExtensions
    {
        public static string GetIcon(this InvestmentTransactionType type)
        {
            return type switch
            {
                InvestmentTransactionType.Buy => "fas fa-shopping-cart",
                InvestmentTransactionType.Sell => "fas fa-money-bill-wave",
                InvestmentTransactionType.Dividend => "fas fa-hand-holding-usd",
                InvestmentTransactionType.Interest => "fas fa-percentage",
                InvestmentTransactionType.Fee => "fas fa-file-invoice-dollar",
                InvestmentTransactionType.Deposit => "fas fa-arrow-circle-down",
                InvestmentTransactionType.Withdrawal => "fas fa-arrow-circle-up",
                InvestmentTransactionType.Income => "fas fa-coins",
                InvestmentTransactionType.Split => "fas fa-project-diagram",
                InvestmentTransactionType.Merger => "fas fa-object-group",
                _ => "fas fa-question-circle"
            };
        }
    }
}