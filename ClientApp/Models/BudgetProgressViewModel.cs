using System;
using System.Collections.Generic;

namespace FinanceManager.ClientApp.Models
{    public class BudgetProgressViewModel
    {
        public string BudgetId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal BudgetAmount { get; set; }
        public decimal CurrentSpent { get; set; }
        public decimal RemainingAmount => BudgetAmount - CurrentSpent;
        public double PercentageUsed => Math.Min(100, Math.Round((double)(CurrentSpent / BudgetAmount * 100), 1));
        public string CategoryName { get; set; } = string.Empty;
        public string? CategoryIcon { get; set; }
        public string? CategoryColor { get; set; }
        public bool IsOverBudget => CurrentSpent > BudgetAmount;
        public double DaysRemainingPercentage { get; set; }
        public string Status => DetermineStatus();

        private string DetermineStatus()
        {
            if (IsOverBudget)
                return "Ultrapassado";
            
            if (PercentageUsed > 90)
                return "Crítico";
            
            if (PercentageUsed > 75)
                return "Atenção";
            
            if (PercentageUsed > DaysRemainingPercentage + 10)
                return "Acima do Esperado";
            
            return "Normal";
        }
    }    public class BudgetDashboardViewModel
    {
        public List<BudgetProgressViewModel> ActiveBudgets { get; set; } = new List<BudgetProgressViewModel>();
        public decimal TotalBudgetAmount { get; set; }
        public decimal TotalSpent { get; set; }
        public decimal RemainingAmount => TotalBudgetAmount - TotalSpent;
        public double OverallPercentage => Math.Min(100, Math.Round((double)(TotalSpent / TotalBudgetAmount * 100), 1));
        public DateTime CurrentPeriodStart { get; set; } = DateTime.Today.AddDays(-(int)DateTime.Today.Day + 1);
        public DateTime CurrentPeriodEnd { get; set; } = DateTime.Today.AddDays(-(int)DateTime.Today.Day + 1).AddMonths(1).AddDays(-1);
        public int RemainingDays { get; set; }
    }
}