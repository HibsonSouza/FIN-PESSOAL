using System;
using System.Collections.Generic;

namespace FinanceManager.ClientApp.Models
{
    public class BudgetProgressViewModel
    {
        public int BudgetId { get; set; }
        public string Name { get; set; }
        public decimal BudgetAmount { get; set; }
        public decimal CurrentSpent { get; set; }
        public decimal RemainingAmount => BudgetAmount - CurrentSpent;
        public double PercentageUsed => Math.Min(100, Math.Round((double)(CurrentSpent / BudgetAmount * 100), 1));
        public string CategoryName { get; set; }
        public string CategoryIcon { get; set; }
        public string CategoryColor { get; set; }
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
    }

    public class BudgetDashboardViewModel
    {
        public List<BudgetProgressViewModel> ActiveBudgets { get; set; } = new List<BudgetProgressViewModel>();
        public decimal TotalBudgetAmount { get; set; }
        public decimal TotalSpent { get; set; }
        public decimal RemainingAmount => TotalBudgetAmount - TotalSpent;
        public double OverallPercentage => Math.Min(100, Math.Round((double)(TotalSpent / TotalBudgetAmount * 100), 1));
        public DateTime CurrentPeriodStart { get; set; }
        public DateTime CurrentPeriodEnd { get; set; }
        public int RemainingDays { get; set; }
    }
}