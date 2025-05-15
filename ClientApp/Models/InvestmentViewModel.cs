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
        Sell
    }
}