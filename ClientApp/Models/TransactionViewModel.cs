using System.ComponentModel.DataAnnotations;

namespace FinanceManager.ClientApp.Models
{    public class TransactionViewModel
    {
        public string Id { get; set; } = string.Empty;
        
        public string Description { get; set; } = string.Empty;
        
        public decimal Amount { get; set; }
        
        public DateTime Date { get; set; }
        
        public TransactionType Type { get; set; }
        
        public string? AccountId { get; set; }
        
        public string? AccountName { get; set; }
        
        public string? CategoryId { get; set; }
        
        public string? CategoryName { get; set; }
        
        public string? CategoryColor { get; set; }
        
        public string? ToAccountId { get; set; }
        
        public string? ToAccountName { get; set; }
        
        public bool IsRecurring { get; set; }
        
        public string? RecurrenceId { get; set; }
        
        public string? RecurrencePattern { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        public bool IsPending { get; set; }
        
        public string? Notes { get; set; }
        
        public List<TagViewModel> Tags { get; set; } = new();
        
        public List<AttachmentViewModel> Attachments { get; set; } = new();
        
        // Propriedades adicionais
        public string? Location { get; set; }
        
        public bool IsReconciled { get; set; } = false;
        
        public string? CreditCardId { get; set; }
        
        public string? ReceiptUrl { get; set; }
        
        // Propriedades para parcelamento
        public bool IsInstallment { get; set; } = false;
        
        public int? TotalInstallments { get; set; }
        
        public int? CurrentInstallment { get; set; }
    }
    
    public class TransactionCreateModel
    {
        [Required(ErrorMessage = "A descrição é obrigatória")]
        [StringLength(100, ErrorMessage = "A descrição deve ter no máximo 100 caracteres")]
        public string Description { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O valor é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero")]
        public decimal Amount { get; set; }
        
        [Required(ErrorMessage = "A data é obrigatória")]
        public DateTime Date { get; set; } = DateTime.Today;
        
        [Required(ErrorMessage = "O tipo de transação é obrigatório")]
        public TransactionType Type { get; set; } = TransactionType.Expense;
        
        [Required(ErrorMessage = "A conta é obrigatória")]
        public string AccountId { get; set; } = string.Empty;
        
        public string? CategoryId { get; set; }
        
        public string? ToAccountId { get; set; }
        
        public bool IsRecurring { get; set; } = false;
        
        public string? RecurrencePattern { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        public bool IsPending { get; set; } = false;
        
        public string? Notes { get; set; }
        
        public List<string> TagIds { get; set; } = new();
    }
      public class TransactionUpdateModel
    {
        [Required(ErrorMessage = "A descrição é obrigatória")]
        [StringLength(100, ErrorMessage = "A descrição deve ter no máximo 100 caracteres")]
        public string Description { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O valor é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero")]
        public decimal Amount { get; set; }
        
        [Required(ErrorMessage = "A data é obrigatória")]
        public DateTime Date { get; set; }
        
        [Required(ErrorMessage = "O tipo de transação é obrigatório")]
        public TransactionType Type { get; set; }
        
        [Required(ErrorMessage = "A conta é obrigatória")]
        public string AccountId { get; set; } = string.Empty;
        
        public string? CategoryId { get; set; }
        
        public string? ToAccountId { get; set; }
        
        public bool IsPending { get; set; }
        
        public string? Notes { get; set; }
        
        public List<string> TagIds { get; set; } = new();
        
        public bool UpdateRecurringSeries { get; set; } = false;
        
        // Propriedades adicionais para compatibilidade
        public string? Location { get; set; }
        
        public bool IsReconciled { get; set; } = false;
        
        public string? CreditCardId { get; set; }
        
        // Alias para compatibilidade
        public IEnumerable<string> Tags
        {
            get => TagIds;
            set => TagIds = value.ToList();
        }
    }
    
    public class TagViewModel
    {
        public string Id { get; set; } = string.Empty;
        
        public string Name { get; set; } = string.Empty;
        
        public string? Color { get; set; }
    }
    
    public class AttachmentViewModel
    {
        public string Id { get; set; } = string.Empty;
        
        public string FileName { get; set; } = string.Empty;
        
        public string ContentType { get; set; } = string.Empty;
        
        public long FileSize { get; set; }
        
        public DateTime UploadDate { get; set; }
        
        public string DownloadUrl { get; set; } = string.Empty;
    }
    
    public class TransactionGroupViewModel
    {
        public string GroupKey { get; set; } = string.Empty;
        
        public string GroupName { get; set; } = string.Empty;
        
        public List<TransactionViewModel> Transactions { get; set; } = new();
        
        public decimal TotalAmount { get; set; }
        
        public decimal PercentageOfTotal { get; set; }
    }
    
    // TransactionFilterModel foi movido para seu próprio arquivo
}