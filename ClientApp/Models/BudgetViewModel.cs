using System.ComponentModel.DataAnnotations;

namespace FinanceManager.ClientApp.Models
{
    public class BudgetViewModel
    {
        public string Id { get; set; } = string.Empty;
        
        public string Name { get; set; } = string.Empty;
        
        public decimal Amount { get; set; }
        
        public decimal SpentAmount { get; set; }
        
        public decimal RemainingAmount { get; set; }
        
        public double PercentUsed { get; set; }
        
        public BudgetPeriod Period { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        
        public bool IsActive { get; set; }
        
        public string? CategoryId { get; set; }
        
        public string? CategoryName { get; set; }
        
        public string? Color { get; set; }
        
        public string? Notes { get; set; }
    }
    
    public class BudgetCreateModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(50, ErrorMessage = "O nome deve ter no máximo 50 caracteres")]
        public string Name { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O valor é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero")]
        public decimal Amount { get; set; }
        
        [Required(ErrorMessage = "O período é obrigatório")]
        public BudgetPeriod Period { get; set; } = BudgetPeriod.Monthly;
        
        public DateTime? StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public string? CategoryId { get; set; }
        
        public string? Color { get; set; }
        
        public string? Notes { get; set; }
    }
    
    public class BudgetUpdateModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(50, ErrorMessage = "O nome deve ter no máximo 50 caracteres")]
        public string Name { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O valor é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero")]
        public decimal Amount { get; set; }
        
        [Required(ErrorMessage = "O período é obrigatório")]
        public BudgetPeriod Period { get; set; }
        
        public DateTime? StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        public bool IsActive { get; set; }
        
        public string? CategoryId { get; set; }
        
        public string? Color { get; set; }
        
        public string? Notes { get; set; }
    }
    
    // BudgetProgressViewModel está definido em seu próprio arquivo
}