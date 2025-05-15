using System;
using System.ComponentModel.DataAnnotations;

namespace FinanceManager.ClientApp.Models
{
    public class TransactionFormModel
    {
        [Required(ErrorMessage = "Selecione uma conta")]
        public int AccountId { get; set; }
        
        public int? CategoryId { get; set; }
        
        [Required(ErrorMessage = "Digite um valor")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero")]
        public decimal Amount { get; set; }
        
        [Required(ErrorMessage = "Selecione um tipo")]
        public TransactionType Type { get; set; }
        
        [Required(ErrorMessage = "Selecione uma data")]
        public DateTime Date { get; set; } = DateTime.Today;
        
        [Required(ErrorMessage = "Digite uma descrição")]
        [StringLength(255, ErrorMessage = "A descrição não pode ter mais de 255 caracteres")]
        public string Description { get; set; }
        
        public string Notes { get; set; }
        
        public bool IsRecurring { get; set; }
        
        public RecurringPeriod? RecurringPeriod { get; set; }
    }
    
    public class BudgetFormModel
    {
        [Required(ErrorMessage = "Digite um nome")]
        [StringLength(100, ErrorMessage = "O nome não pode ter mais de 100 caracteres")]
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        [Required(ErrorMessage = "Digite um valor")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero")]
        public decimal Amount { get; set; }
        
        [Required(ErrorMessage = "Selecione um período")]
        public BudgetPeriod Period { get; set; }
        
        [Required(ErrorMessage = "Selecione uma data de início")]
        public DateTime StartDate { get; set; } = DateTime.Today;
        
        public DateTime? EndDate { get; set; }
        
        public int? CategoryId { get; set; }
        
        public bool IsRecurring { get; set; }
    }
    
    public class InvestmentFormModel
    {
        [Required(ErrorMessage = "Digite um nome")]
        [StringLength(100, ErrorMessage = "O nome não pode ter mais de 100 caracteres")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Selecione um tipo")]
        public InvestmentType Type { get; set; }
        
        [Required(ErrorMessage = "Digite um valor inicial")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero")]
        public decimal InitialAmount { get; set; }
        
        [Required(ErrorMessage = "Digite um valor atual")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero")]
        public decimal CurrentValue { get; set; }
        
        [Required(ErrorMessage = "Selecione uma data de início")]
        public DateTime StartDate { get; set; } = DateTime.Today;
        
        public DateTime? EndDate { get; set; }
        
        public decimal ReturnRate { get; set; }
        
        public string Notes { get; set; }
    }
    
    public class InvestmentTransactionFormModel
    {
        [Required(ErrorMessage = "Selecione um tipo")]
        public InvestmentTransactionType Type { get; set; }
        
        [Required(ErrorMessage = "Digite um valor")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero")]
        public decimal Amount { get; set; }
        
        [Required(ErrorMessage = "Digite a quantidade de ações/cotas")]
        [Range(0.0001, double.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero")]
        public decimal Shares { get; set; }
        
        [Required(ErrorMessage = "Digite o preço por ação/cota")]
        [Range(0.0001, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero")]
        public decimal PricePerShare { get; set; }
        
        [Required(ErrorMessage = "Selecione uma data")]
        public DateTime Date { get; set; } = DateTime.Today;
        
        public string Notes { get; set; }
    }
}