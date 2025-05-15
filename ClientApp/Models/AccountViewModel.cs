using System.ComponentModel.DataAnnotations;

namespace FinanceManager.ClientApp.Models
{
    public class AccountViewModel
    {
        public string Id { get; set; } = string.Empty;
        
        public string Name { get; set; } = string.Empty;
        
        public string Type { get; set; } = string.Empty;
        
        public string BankName { get; set; } = string.Empty;
        
        public decimal Balance { get; set; }
        
        public bool IsActive { get; set; }
        
        public string? Color { get; set; }
        
        public string? Description { get; set; }
        
        public List<TransactionViewModel> RecentTransactions { get; set; } = new();
    }
    
    public class AccountCreateModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(50, ErrorMessage = "O nome deve ter no máximo 50 caracteres")]
        public string Name { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O tipo é obrigatório")]
        public string Type { get; set; } = "Corrente";
        
        [Required(ErrorMessage = "O nome do banco é obrigatório")]
        [StringLength(50, ErrorMessage = "O nome do banco deve ter no máximo 50 caracteres")]
        public string BankName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O saldo inicial é obrigatório")]
        public decimal InitialBalance { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public string? Color { get; set; }
        
        public string? Description { get; set; }
    }
    
    public class AccountUpdateModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(50, ErrorMessage = "O nome deve ter no máximo 50 caracteres")]
        public string Name { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O tipo é obrigatório")]
        public string Type { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O nome do banco é obrigatório")]
        [StringLength(50, ErrorMessage = "O nome do banco deve ter no máximo 50 caracteres")]
        public string BankName { get; set; } = string.Empty;
        
        public bool IsActive { get; set; }
        
        public string? Color { get; set; }
        
        public string? Description { get; set; }
    }
}