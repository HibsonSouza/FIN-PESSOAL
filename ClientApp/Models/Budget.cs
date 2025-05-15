using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinanceManager.ClientApp.Models
{
    public class BudgetViewModel
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "Name is too long.")]
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        [Required]
        public decimal Amount { get; set; }
        
        public decimal Spent { get; set; }
        
        public decimal Remaining => Amount - Spent;
        
        public double PercentUsed => Amount > 0 ? (double)(Spent / Amount) * 100 : 0;
        
        [Required]
        public BudgetPeriod Period { get; set; }
        
        [Required]
        public DateTime StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        public int? CategoryId { get; set; }
        
        public CategoryViewModel Category { get; set; }
        
        public bool IsRecurring { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedAt { get; set; }
        
        public DateTime? UpdatedAt { get; set; }
        
        // Navigation property
        public List<BudgetItemViewModel> BudgetItems { get; set; }
    }
    
    public class BudgetCreateModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "Name is too long.")]
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        [Required]
        public decimal Amount { get; set; }
        
        [Required]
        public BudgetPeriod Period { get; set; }
        
        [Required]
        public DateTime StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        public int? CategoryId { get; set; }
        
        public bool IsRecurring { get; set; }
    }
    
    public class BudgetUpdateModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "Name is too long.")]
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        [Required]
        public decimal Amount { get; set; }
        
        [Required]
        public BudgetPeriod Period { get; set; }
        
        [Required]
        public DateTime StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        public int? CategoryId { get; set; }
        
        public bool IsRecurring { get; set; }
        
        public bool IsActive { get; set; }
    }
    
    public class BudgetItemViewModel
    {
        public int Id { get; set; }
        
        [Required]
        public int BudgetId { get; set; }
        
        public BudgetViewModel Budget { get; set; }
        
        [Required]
        public int CategoryId { get; set; }
        
        public CategoryViewModel Category { get; set; }
        
        [Required]
        public decimal Amount { get; set; }
        
        public decimal Spent { get; set; }
        
        public decimal Remaining => Amount - Spent;
        
        public double PercentUsed => Amount > 0 ? (double)(Spent / Amount) * 100 : 0;
        
        public DateTime CreatedAt { get; set; }
        
        public DateTime? UpdatedAt { get; set; }
    }
    
    public enum BudgetPeriod
    {
        Daily,
        Weekly,
        Monthly,
        Quarterly,
        Yearly,
        Custom
    }
}