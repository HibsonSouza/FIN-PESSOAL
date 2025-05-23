using System;
using System.Collections.Generic;

namespace FinanceManager.ClientApp.Models
{
    public class InvestmentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public InvestmentType Type { get; set; }
        public InvestmentRiskLevel RiskLevel { get; set; }
        public decimal InitialValue { get; set; }
        public decimal CurrentValue { get; set; }
        public decimal Profitability { get; set; }  // Em percentual
        public decimal PerformancePercentage => InitialValue != 0 ? (CurrentValue - InitialValue) / InitialValue * 100 : 0; // Proteção contra divisão por zero
        public string Institution { get; set; } = string.Empty;
        public DateTime StartDate { get; set; } = DateTime.Today; // Inicializado com DateTime.Today
        public DateTime? MaturityDate { get; set; }
        public string Icon { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public List<InvestmentTransactionViewModel> Transactions { get; set; } = new List<InvestmentTransactionViewModel>();
        
        // Propriedades adicionais necessárias
        public string Notes { get; set; } = string.Empty;
        public InvestmentRiskLevel Risk => RiskLevel;
        public int? LiquidityDays { get; set; }
        
        // Propriedade adicional para compatibilidade com InvestmentDetails.razor
        public DateTime AcquisitionDate { get => StartDate; set => StartDate = value; }
        
        // Propriedade para cálculo de ganho/perda
        public decimal GainLoss => CurrentValue - InitialValue;
        
        // Propriedade para cálculo de ganho/perda percentual
        public decimal GainLossPercentage => PerformancePercentage;
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
    
    public static class InvestmentTypeExtensions
    {
        public static string ToDisplayString(this InvestmentType type)
        {
            return type switch
            {
                InvestmentType.SavingsAccount => "Poupança",
                InvestmentType.FixedIncome => "Renda Fixa",
                InvestmentType.Stock => "Ações",
                InvestmentType.Fund => "Fundos",
                InvestmentType.RealEstate => "Imóveis",
                InvestmentType.Cryptocurrency => "Criptomoedas",
                InvestmentType.Other => "Outros",
                _ => "Desconhecido"
            };
        }
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
        public string Id { get; set; } = string.Empty;
        public string InvestmentId { get; set; } = string.Empty;
        public string InvestmentName { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public InvestmentTransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public decimal Price { get; set; } // Preço por unidade
        public decimal Quantity { get; set; }
        public decimal Taxes { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public decimal BalanceAfterTransaction { get; set; }
        
        // Alias para compatibilidade
        public decimal UnitPrice { 
            get => Price; 
            set => Price = value; 
        }
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