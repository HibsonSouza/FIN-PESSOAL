using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceManager.Models
{
    /// <summary>
    /// Entidade que representa uma meta financeira
    /// </summary>
    public class Goal
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal TargetAmount { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal CurrentAmount { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime TargetDate { get; set; }
        
        [StringLength(255)]
        public string? Description { get; set; }
        
        [StringLength(50)]
        public string? Color { get; set; }
        
        [StringLength(50)]
        public string? Icon { get; set; }
        
        public int UserId { get; set; }
        
        [ForeignKey("UserId")]
        public User User { get; set; } = null!;
        
        public int? AccountId { get; set; }
        
        [ForeignKey("AccountId")]
        public Account? Account { get; set; }
        
        public bool IsCompleted { get; set; } = false;
        
        public DateTime? CompletedDate { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? UpdatedAt { get; set; }
    }
}
