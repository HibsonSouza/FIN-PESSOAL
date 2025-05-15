using System;
using System.ComponentModel.DataAnnotations;

namespace FinanceManager.ClientApp.Models
{
    public class TransactionViewModel
    {
        public int Id { get; set; }
        
        [Required]
        public int AccountId { get; set; }
        public AccountViewModel Account { get; set; }
        
        public int? CategoryId { get; set; }
        public CategoryViewModel Category { get; set; }
        
        [Required]
        public decimal Amount { get; set; }
        
        [Required]
        public TransactionType Type { get; set; }
        
        [Required]
        public DateTime Date { get; set; }
        
        [StringLength(255)]
        public string Description { get; set; }
        
        public string Notes { get; set; }
        
        public bool IsRecurring { get; set; }
        
        public RecurringPeriod? RecurringPeriod { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public DateTime? UpdatedAt { get; set; }
    }
    
    public class TransactionCreateModel
    {
        [Required]
        public int AccountId { get; set; }
        
        public int? CategoryId { get; set; }
        
        [Required]
        public decimal Amount { get; set; }
        
        [Required]
        public TransactionType Type { get; set; }
        
        [Required]
        public DateTime Date { get; set; }
        
        [StringLength(255)]
        public string Description { get; set; }
        
        public string Notes { get; set; }
        
        public bool IsRecurring { get; set; }
        
        public RecurringPeriod? RecurringPeriod { get; set; }
    }
    
    public class TransactionUpdateModel
    {
        [Required]
        public int AccountId { get; set; }
        
        public int? CategoryId { get; set; }
        
        [Required]
        public decimal Amount { get; set; }
        
        [Required]
        public TransactionType Type { get; set; }
        
        [Required]
        public DateTime Date { get; set; }
        
        [StringLength(255)]
        public string Description { get; set; }
        
        public string Notes { get; set; }
        
        public bool IsRecurring { get; set; }
        
        public RecurringPeriod? RecurringPeriod { get; set; }
    }
    
    public enum TransactionType
    {
        Income,
        Expense,
        Transfer
    }
    
    public enum RecurringPeriod
    {
        Daily,
        Weekly,
        Biweekly,
        Monthly,
        Quarterly,
        Yearly
    }
    
    public class DateRange
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}