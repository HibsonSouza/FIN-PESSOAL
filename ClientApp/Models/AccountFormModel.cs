using System.ComponentModel.DataAnnotations;

namespace FinanceManager.ClientApp.Models
{    /// <summary>
    /// Modelo para criar ou editar uma conta financeira
    /// </summary>
    public class AccountFormModel
    {
        // ID da conta (necessário para atualização)
        public string? Id { get; set; }
        
        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, ErrorMessage = "Nome não pode exceder 100 caracteres")]
        public string Name { get; set; } = string.Empty;
          [Required(ErrorMessage = "Tipo de conta é obrigatório")]
        public AccountType Type { get; set; } = AccountType.Checking;
        
        [Required(ErrorMessage = "Saldo inicial é obrigatório")]
        public decimal Balance { get; set; }
        
        public string? Institution { get; set; }
        
        public string? AccountNumber { get; set; }
        
        public string? Agency { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public string IconName { get; set; } = string.Empty;
        
        public string Color { get; set; } = "#1976d2";
        
        public string? Description { get; set; }
        
        public bool IncludeInTotal { get; set; } = true;
    }
}
