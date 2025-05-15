using System;

namespace FinanceManager.ClientApp.Models
{
    public class BudgetViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public BudgetPeriod Period { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public decimal CurrentSpent { get; set; }
        public decimal RemainingAmount => Amount - CurrentSpent;
        public double PercentageUsed => (double)(CurrentSpent / Amount * 100);
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }

    public enum BudgetPeriod
    {
        Monthly,
        Quarterly,
        Yearly
    }

    public class BudgetCreateModel
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public BudgetPeriod Period { get; set; }
        public int? CategoryId { get; set; }
    }

    public class BudgetUpdateModel
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public BudgetPeriod Period { get; set; }
        public int? CategoryId { get; set; }
        public bool IsActive { get; set; }
    }
}