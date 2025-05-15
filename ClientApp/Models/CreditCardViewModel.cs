using System.ComponentModel.DataAnnotations;

namespace FinanceManager.ClientApp.Models
{
    public class CreditCardViewModel
    {
        public string Id { get; set; } = string.Empty;
        
        public string Name { get; set; } = string.Empty;
        
        public string Number { get; set; } = string.Empty;
        
        public string LastFourDigits { get; set; } = string.Empty;
        
        public decimal CreditLimit { get; set; }
        
        public decimal CurrentBalance { get; set; }
        
        public decimal AvailableCredit { get; set; }
        
        public DateTime DueDate { get; set; }
        
        public DateTime ClosingDate { get; set; }
        
        public bool IsActive { get; set; }
        
        public string BankName { get; set; } = string.Empty;
        
        public string? Color { get; set; }
        
        public string? Description { get; set; }
        
        public string? AccountId { get; set; }
        
        public string? AccountName { get; set; }
    }
    
    public class CreditCardCreateModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(50, ErrorMessage = "O nome deve ter no máximo 50 caracteres")]
        public string Name { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O número do cartão é obrigatório")]
        [StringLength(19, ErrorMessage = "O número do cartão deve ter entre 13 e 19 caracteres", MinimumLength = 13)]
        [RegularExpression(@"^[\d\s-]+$", ErrorMessage = "O número do cartão deve conter apenas números, espaços ou hífens")]
        public string Number { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O limite de crédito é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O limite de crédito deve ser maior que zero")]
        public decimal CreditLimit { get; set; }
        
        [Required(ErrorMessage = "O saldo atual é obrigatório")]
        public decimal CurrentBalance { get; set; }
        
        [Required(ErrorMessage = "A data de vencimento é obrigatória")]
        [Range(1, 31, ErrorMessage = "A data de vencimento deve estar entre 1 e 31")]
        public int DueDay { get; set; }
        
        [Required(ErrorMessage = "A data de fechamento é obrigatória")]
        [Range(1, 31, ErrorMessage = "A data de fechamento deve estar entre 1 e 31")]
        public int ClosingDay { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        [Required(ErrorMessage = "O nome do banco é obrigatório")]
        [StringLength(50, ErrorMessage = "O nome do banco deve ter no máximo 50 caracteres")]
        public string BankName { get; set; } = string.Empty;
        
        public string? Color { get; set; }
        
        public string? Description { get; set; }
        
        public string? AccountId { get; set; }
    }
    
    public class CreditCardUpdateModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(50, ErrorMessage = "O nome deve ter no máximo 50 caracteres")]
        public string Name { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O limite de crédito é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O limite de crédito deve ser maior que zero")]
        public decimal CreditLimit { get; set; }
        
        [Required(ErrorMessage = "A data de vencimento é obrigatória")]
        [Range(1, 31, ErrorMessage = "A data de vencimento deve estar entre 1 e 31")]
        public int DueDay { get; set; }
        
        [Required(ErrorMessage = "A data de fechamento é obrigatória")]
        [Range(1, 31, ErrorMessage = "A data de fechamento deve estar entre 1 e 31")]
        public int ClosingDay { get; set; }
        
        public bool IsActive { get; set; }
        
        [Required(ErrorMessage = "O nome do banco é obrigatório")]
        [StringLength(50, ErrorMessage = "O nome do banco deve ter no máximo 50 caracteres")]
        public string BankName { get; set; } = string.Empty;
        
        public string? Color { get; set; }
        
        public string? Description { get; set; }
        
        public string? AccountId { get; set; }
    }
    
    public class CreditCardTransactionViewModel
    {
        public string Id { get; set; } = string.Empty;
        
        public string Description { get; set; } = string.Empty;
        
        public decimal Amount { get; set; }
        
        public DateTime Date { get; set; }
        
        public string? CategoryId { get; set; }
        
        public string? CategoryName { get; set; }
        
        public string CreditCardId { get; set; } = string.Empty;
        
        public bool IsInstallment { get; set; }
        
        public int? InstallmentNumber { get; set; }
        
        public int? TotalInstallments { get; set; }
    }
    
    public class CreditCardTransactionCreateModel
    {
        [Required(ErrorMessage = "A descrição é obrigatória")]
        [StringLength(100, ErrorMessage = "A descrição deve ter no máximo 100 caracteres")]
        public string Description { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O valor é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero")]
        public decimal Amount { get; set; }
        
        [Required(ErrorMessage = "A data é obrigatória")]
        public DateTime Date { get; set; } = DateTime.Today;
        
        public string? CategoryId { get; set; }
        
        [Required(ErrorMessage = "O cartão de crédito é obrigatório")]
        public string CreditCardId { get; set; } = string.Empty;
        
        public bool IsInstallment { get; set; } = false;
        
        public int? InstallmentNumber { get; set; }
        
        public int? TotalInstallments { get; set; }
    }
}