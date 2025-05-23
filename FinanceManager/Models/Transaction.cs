using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FinanceManager.Models.Enums;

namespace FinanceManager.Models
{
    /// <summary>
    /// Entidade que representa uma transação financeira
    /// </summary>
    public class Transaction
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Description { get; set; } = string.Empty;
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        
        public DateTime Date { get; set; }
        
        public TransactionType Type { get; set; }
        
        public int CategoryId { get; set; }
        
        [ForeignKey("CategoryId")]
        public Category Category { get; set; } = null!;
        
        public int AccountId { get; set; }
        
        [ForeignKey("AccountId")]
        public Account Account { get; set; } = null!;
        
        public int? DestinationAccountId { get; set; }
        
        [ForeignKey("DestinationAccountId")]
        public Account? DestinationAccount { get; set; }
        
        public bool IsPaid { get; set; } = true;
        
        [StringLength(500)]
        public string? Notes { get; set; }
        
        public bool IsRecurring { get; set; } = false;
        
        public RecurringPeriod? RecurringPeriod { get; set; }
        
        public int? RecurringFrequency { get; set; }
        
        public DateTime? NextRecurringDate { get; set; }
        
        public int? ParentTransactionId { get; set; }
        
        public int UserId { get; set; }
        
        [ForeignKey("UserId")]
        public User User { get; set; } = null!;
        
        [StringLength(100)]
        public string? Tags { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? UpdatedAt { get; set; }
    }
}
