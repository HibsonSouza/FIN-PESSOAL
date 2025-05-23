using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FinanceManager.Models.Enums;

namespace FinanceManager.Models
{
    /// <summary>
    /// Entidade que representa um cartão de crédito
    /// </summary>
    public class CreditCard
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Limit { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal AvailableLimit { get; set; }
        
        public int? Day { get; set; }
        
        public int? ClosingDay { get; set; }
        
        [StringLength(50)]
        public string? Color { get; set; }
        
        [StringLength(50)]
        public string? BankName { get; set; }
        
        [StringLength(50)]
        public string? LastFourDigits { get; set; }
        
        public int? AccountId { get; set; }
        
        [ForeignKey("AccountId")]
        public Account? Account { get; set; }
        
        public int UserId { get; set; }
        
        [ForeignKey("UserId")]
        public User User { get; set; } = null!;
        
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? UpdatedAt { get; set; }
    }
}
