using System.ComponentModel.DataAnnotations;

namespace FinanceManager.ClientApp.Models
{
    public class GoalViewModel
    {
        public string Id { get; set; } = string.Empty;
        
        public string Name { get; set; } = string.Empty;
        
        public string Description { get; set; } = string.Empty;
        
        public decimal TargetAmount { get; set; }
        
        public decimal CurrentAmount { get; set; }
        
        public decimal CompletionPercentage { get; set; }
        
        public DateTime? TargetDate { get; set; }
        
        public bool IsCompleted { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        public string? AccountId { get; set; }
        
        public string? AccountName { get; set; }
        
        public string? CategoryId { get; set; }
        
        public string? CategoryName { get; set; }
        
        public string? Icon { get; set; }
        
        public string? Color { get; set; }
        
        public List<GoalContributionViewModel> Contributions { get; set; } = new();
    }
    
    public class GoalCreateModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(50, ErrorMessage = "O nome deve ter no máximo 50 caracteres")]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(200, ErrorMessage = "A descrição deve ter no máximo 200 caracteres")]
        public string Description { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O valor alvo é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor alvo deve ser maior que zero")]
        public decimal TargetAmount { get; set; }
        
        public decimal InitialAmount { get; set; } = 0;
        
        public DateTime? TargetDate { get; set; }
        
        public string? AccountId { get; set; }
        
        public string? CategoryId { get; set; }
        
        public string? Icon { get; set; }
        
        public string? Color { get; set; }
    }
    
    public class GoalUpdateModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(50, ErrorMessage = "O nome deve ter no máximo 50 caracteres")]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(200, ErrorMessage = "A descrição deve ter no máximo 200 caracteres")]
        public string Description { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O valor alvo é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor alvo deve ser maior que zero")]
        public decimal TargetAmount { get; set; }
        
        public DateTime? TargetDate { get; set; }
        
        public string? AccountId { get; set; }
        
        public string? CategoryId { get; set; }
        
        public string? Icon { get; set; }
        
        public string? Color { get; set; }
    }
    
    public class GoalContributionModel
    {
        [Required(ErrorMessage = "O valor é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero")]
        public decimal Amount { get; set; }
        
        [Required(ErrorMessage = "A data é obrigatória")]
        public DateTime Date { get; set; } = DateTime.Today;
        
        public string? Notes { get; set; }
        
        public string? TransactionId { get; set; }
    }
    
    public class GoalContributionViewModel
    {
        public string Id { get; set; } = string.Empty;
        
        public decimal Amount { get; set; }
        
        public DateTime Date { get; set; }
        
        public string? Notes { get; set; }
        
        public string? TransactionId { get; set; }
    }
}