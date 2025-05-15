using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinanceManager.ClientApp.Models
{
    public class AccountViewModel
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "Name is too long.")]
        public string Name { get; set; }
        
        [Required]
        public decimal Balance { get; set; }
        
        public AccountType Type { get; set; }
        
        public string Notes { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public DateTime? UpdatedAt { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        // Navigation property for transactions
        public List<TransactionViewModel> Transactions { get; set; }
    }
    
    public class AccountCreateModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "Name is too long.")]
        public string Name { get; set; }
        
        [Required]
        public decimal Balance { get; set; }
        
        public AccountType Type { get; set; }
        
        public string Notes { get; set; }
    }
    
    public class AccountUpdateModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "Name is too long.")]
        public string Name { get; set; }
        
        [Required]
        public decimal Balance { get; set; }
        
        public AccountType Type { get; set; }
        
        public string Notes { get; set; }
        
        public bool IsActive { get; set; }
    }
    
    public class AccountBalanceHistoryViewModel
    {
        public DateTime Date { get; set; }
        public decimal Balance { get; set; }
    }
    
    public enum AccountType
    {
        Checking,
        Savings,
        CreditCard,
        Investment,
        Cash,
        Other
    }
}