using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinanceManager.ClientApp.Models
{
    public class CreditCardViewModel
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "Name is too long.")]
        public string Name { get; set; }
        
        [Required]
        public decimal CreditLimit { get; set; }
        
        [Required]
        public decimal CurrentBalance { get; set; }
        
        [Required]
        public decimal AvailableCredit { get; set; }
        
        [Required]
        public decimal AnnualFee { get; set; }
        
        [Required]
        public decimal APR { get; set; }
        
        [Required]
        public int DueDay { get; set; }
        
        [Required]
        public int ClosingDay { get; set; }
        
        public string Notes { get; set; }
        
        public string Color { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedAt { get; set; }
        
        public DateTime? UpdatedAt { get; set; }
        
        // Navigation properties
        public List<TransactionViewModel> Transactions { get; set; }
        public List<CreditCardBillViewModel> Bills { get; set; }
    }
    
    public class CreditCardCreateModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "Name is too long.")]
        public string Name { get; set; }
        
        [Required]
        public decimal CreditLimit { get; set; }
        
        [Required]
        public decimal CurrentBalance { get; set; }
        
        [Required]
        public decimal AnnualFee { get; set; }
        
        [Required]
        public decimal APR { get; set; }
        
        [Required]
        [Range(1, 31, ErrorMessage = "Due day must be between 1 and 31.")]
        public int DueDay { get; set; }
        
        [Required]
        [Range(1, 31, ErrorMessage = "Closing day must be between 1 and 31.")]
        public int ClosingDay { get; set; }
        
        public string Notes { get; set; }
        
        public string Color { get; set; }
    }
    
    public class CreditCardUpdateModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "Name is too long.")]
        public string Name { get; set; }
        
        [Required]
        public decimal CreditLimit { get; set; }
        
        [Required]
        public decimal CurrentBalance { get; set; }
        
        [Required]
        public decimal AnnualFee { get; set; }
        
        [Required]
        public decimal APR { get; set; }
        
        [Required]
        [Range(1, 31, ErrorMessage = "Due day must be between 1 and 31.")]
        public int DueDay { get; set; }
        
        [Required]
        [Range(1, 31, ErrorMessage = "Closing day must be between 1 and 31.")]
        public int ClosingDay { get; set; }
        
        public string Notes { get; set; }
        
        public string Color { get; set; }
        
        public bool IsActive { get; set; }
    }
    
    public class CreditCardStatementModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal TotalDue { get; set; }
        public decimal MinimumDue { get; set; }
    }
    
    public class CreditCardStatementViewModel
    {
        public int Id { get; set; }
        public int CreditCardId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal TotalDue { get; set; }
        public decimal MinimumDue { get; set; }
        public decimal AmountPaid { get; set; }
        public bool IsPaid { get; set; }
        public List<TransactionViewModel> Transactions { get; set; }
    }
    
    public class CreditCardBillViewModel
    {
        public int Id { get; set; }
        public int CreditCardId { get; set; }
        public CreditCardViewModel CreditCard { get; set; }
        
        [Required]
        public DateTime DueDate { get; set; }
        
        [Required]
        public decimal TotalAmount { get; set; }
        
        [Required]
        public decimal MinimumPayment { get; set; }
        
        public decimal AmountPaid { get; set; }
        
        public DateTime? PaymentDate { get; set; }
        
        public bool IsPaid { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public DateTime? UpdatedAt { get; set; }
    }
}