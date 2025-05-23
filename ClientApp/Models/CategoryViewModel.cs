using System.ComponentModel.DataAnnotations;

namespace FinanceManager.ClientApp.Models
{
    public class CategoryViewModel
    {
        public string Id { get; set; } = string.Empty;
        
        public string Name { get; set; } = string.Empty;
        
        public TransactionType Type { get; set; }
        
        public string? Color { get; set; }
        
        public string? Icon { get; set; }
        
        public string? ParentCategoryId { get; set; }
        
        public string? ParentCategoryName { get; set; }
        
        public bool IsSystem { get; set; }
    }
    
    public class CategoryCreateModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(50, ErrorMessage = "O nome deve ter no máximo 50 caracteres")]
        public string Name { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O tipo é obrigatório")]
        public TransactionType Type { get; set; } = TransactionType.Expense;
        
        public string? Color { get; set; }
        
        public string? Icon { get; set; }
        
        public string? ParentCategoryId { get; set; }
    }
    
    public class CategoryUpdateModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(50, ErrorMessage = "O nome deve ter no máximo 50 caracteres")]
        public string Name { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O tipo é obrigatório")]
        public TransactionType Type { get; set; }
        
        public string? Color { get; set; }
        
        public string? Icon { get; set; }
        
        public string? ParentCategoryId { get; set; }    }
      public class CategoryStatisticsModel
    {
        public string CategoryId { get; set; } = string.Empty;
        
        public string CategoryName { get; set; } = string.Empty;
        
        public string? Color { get; set; }
        
        public string? Icon { get; set; }
        
        public TransactionType Type { get; set; }
        
        public decimal TotalAmount { get; set; }
        
        public double Percentage { get; set; }
    }
}