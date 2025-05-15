using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinanceManager.ClientApp.Models
{
    // Modelos principais para cartão de crédito
    public class CreditCardViewModel
    {
        public string Id { get; set; } = string.Empty;
        
        public string Name { get; set; } = string.Empty;
        
        public string Bank { get; set; } = string.Empty;
        
        public string LastFourDigits { get; set; } = string.Empty;
        
        public decimal Limit { get; set; }
        
        public decimal AvailableCredit { get; set; }
        
        public decimal CurrentBalance { get; set; }
        
        public int ClosingDay { get; set; }
        
        public int DueDay { get; set; }
        
        public string? Color { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public DateTime? LastUpdated { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public string? Notes { get; set; }
        
        public List<CreditCardBillViewModel> RecentBills { get; set; } = new List<CreditCardBillViewModel>();
    }
    
    // Formulário para criação e edição
    public class CreditCardFormModel
    {
        [Required(ErrorMessage = "Nome do cartão é obrigatório")]
        [StringLength(100, ErrorMessage = "Nome não pode exceder 100 caracteres")]
        public string Name { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Nome do banco é obrigatório")]
        [StringLength(100, ErrorMessage = "Nome do banco não pode exceder 100 caracteres")]
        public string Bank { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Últimos 4 dígitos são obrigatórios")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Informe os 4 últimos dígitos do cartão")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Apenas números são permitidos")]
        public string LastFourDigits { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Limite de crédito é obrigatório")]
        [Range(0.01, 1000000, ErrorMessage = "Limite deve ser maior que zero")]
        public decimal Limit { get; set; }
        
        [Required(ErrorMessage = "Dia de fechamento é obrigatório")]
        [Range(1, 31, ErrorMessage = "Dia de fechamento deve estar entre 1 e 31")]
        public int ClosingDay { get; set; }
        
        [Required(ErrorMessage = "Dia de vencimento é obrigatório")]
        [Range(1, 31, ErrorMessage = "Dia de vencimento deve estar entre 1 e 31")]
        public int DueDay { get; set; }
        
        [StringLength(20, ErrorMessage = "Cor não pode exceder 20 caracteres")]
        public string? Color { get; set; }
        
        [StringLength(500, ErrorMessage = "Notas não podem exceder 500 caracteres")]
        public string? Notes { get; set; }
    }
    
    // Modelos para criação e atualização
    public class CreditCardCreateModel
    {
        [Required(ErrorMessage = "Nome do cartão é obrigatório")]
        public string Name { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Nome do banco é obrigatório")]
        public string Bank { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Últimos 4 dígitos são obrigatórios")]
        public string LastFourDigits { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Limite de crédito é obrigatório")]
        public decimal Limit { get; set; }
        
        [Required(ErrorMessage = "Dia de fechamento é obrigatório")]
        public int ClosingDay { get; set; }
        
        [Required(ErrorMessage = "Dia de vencimento é obrigatório")]
        public int DueDay { get; set; }
        
        public string? Color { get; set; }
        
        public string? Notes { get; set; }
    }
    
    public class CreditCardUpdateModel
    {
        public string Name { get; set; } = string.Empty;
        
        public string Bank { get; set; } = string.Empty;
        
        public decimal Limit { get; set; }
        
        public int ClosingDay { get; set; }
        
        public int DueDay { get; set; }
        
        public string? Color { get; set; }
        
        public string? Notes { get; set; }
        
        public bool IsActive { get; set; } = true;
    }
    
    // Modelos para fatura do cartão
    public class CreditCardBillViewModel
    {
        public string Id { get; set; } = string.Empty;
        
        public string CreditCardId { get; set; } = string.Empty;
        
        public string CreditCardName { get; set; } = string.Empty;
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        
        public DateTime DueDate { get; set; }
        
        public decimal TotalAmount { get; set; }
        
        public decimal PaidAmount { get; set; }
        
        public decimal RemainingAmount => TotalAmount - PaidAmount;
        
        public bool IsPaid { get; set; }
        
        public string Status => IsPaid ? "Pago" : DateTime.Now > DueDate ? "Atrasado" : "Aberto";
        
        public List<TransactionViewModel> Transactions { get; set; } = new List<TransactionViewModel>();
    }
    
    // Modelos para transações de cartão de crédito
    public class CreditCardTransactionViewModel
    {
        public string Id { get; set; } = string.Empty;
        
        public string Description { get; set; } = string.Empty;
        
        public decimal Amount { get; set; }
        
        public DateTime Date { get; set; }
        
        public string? Category { get; set; }
        
        public string CreditCardId { get; set; } = string.Empty;
        
        public string CreditCardName { get; set; } = string.Empty;
        
        public bool IsInstallment { get; set; }
        
        public int InstallmentNumber { get; set; }
        
        public int TotalInstallments { get; set; }
    }
    
    public class CreditCardTransactionCreateModel
    {
        [Required(ErrorMessage = "Descrição é obrigatória")]
        [StringLength(200, ErrorMessage = "Descrição não pode exceder 200 caracteres")]
        public string Description { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Valor é obrigatório")]
        [Range(0.01, 1000000, ErrorMessage = "Valor deve ser maior que zero")]
        public decimal Amount { get; set; }
        
        [Required(ErrorMessage = "Data é obrigatória")]
        public DateTime Date { get; set; } = DateTime.Now;
        
        public string? CategoryId { get; set; }
        
        public bool IsInstallment { get; set; }
        
        public int TotalInstallments { get; set; } = 1;
    }
}