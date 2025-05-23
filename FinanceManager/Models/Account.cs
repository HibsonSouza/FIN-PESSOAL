using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FinanceManager.Models.Enums;

namespace FinanceManager.Models
{
    /// <summary>
    /// Entidade que representa uma conta financeira (corrente, poupança, etc.)
    /// </summary>
    public class Account
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }
        
        [StringLength(255)]
        public string? Description { get; set; }
        
        public AccountType Type { get; set; }
        
        [StringLength(50)]
        public string? BankName { get; set; }
        
        [StringLength(50)]
        public string? AccountNumber { get; set; }
        
        [StringLength(50)]
        public string? Agency { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        [StringLength(50)]
        public string? Color { get; set; }
        
        public int UserId { get; set; }
        
        [ForeignKey("UserId")]
        public User User { get; set; } = null!;
        
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? UpdatedAt { get; set; }
    }
}
