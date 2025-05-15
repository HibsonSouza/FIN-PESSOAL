using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinanceManager.ClientApp.Models
{
    public class InvestmentViewModel
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "Name is too long.")]
        public string Name { get; set; }
        
        [Required]
        public InvestmentType Type { get; set; }
        
        [Required]
        public decimal InitialAmount { get; set; }
        
        [Required]
        public decimal CurrentValue { get; set; }
        
        [Required]
        public DateTime StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        public decimal ReturnRate { get; set; }
        
        public string Notes { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedAt { get; set; }
        
        public DateTime? UpdatedAt { get; set; }
        
        // Navigation property
        public List<InvestmentTransactionViewModel> Transactions { get; set; }
    }
    
    public class InvestmentCreateModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "Name is too long.")]
        public string Name { get; set; }
        
        [Required]
        public InvestmentType Type { get; set; }
        
        [Required]
        public decimal InitialAmount { get; set; }
        
        [Required]
        public decimal CurrentValue { get; set; }
        
        [Required]
        public DateTime StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        public decimal ReturnRate { get; set; }
        
        public string Notes { get; set; }
    }
    
    public class InvestmentUpdateModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "Name is too long.")]
        public string Name { get; set; }
        
        [Required]
        public InvestmentType Type { get; set; }
        
        [Required]
        public decimal CurrentValue { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        public decimal ReturnRate { get; set; }
        
        public string Notes { get; set; }
        
        public bool IsActive { get; set; }
    }
    
    public class InvestmentTransactionViewModel
    {
        public int Id { get; set; }
        
        [Required]
        public int InvestmentId { get; set; }
        
        public InvestmentViewModel Investment { get; set; }
        
        [Required]
        public InvestmentTransactionType Type { get; set; }
        
        [Required]
        public decimal Amount { get; set; }
        
        [Required]
        public decimal Shares { get; set; }
        
        [Required]
        public decimal PricePerShare { get; set; }
        
        [Required]
        public DateTime Date { get; set; }
        
        public string Notes { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public DateTime? UpdatedAt { get; set; }
    }
    
    public enum InvestmentType
    {
        Stocks,
        Bonds,
        MutualFunds,
        ETFs,
        RealEstate,
        CDs,
        Retirement,
        Cryptocurrency,
        Other
    }
    
    public enum InvestmentTransactionType
    {
        Buy,
        Sell,
        Dividend,
        Interest,
        Split,
        Fee,
        Transfer,
        Other
    }
}