using System.ComponentModel.DataAnnotations;

namespace FinanceManager.ClientApp.Models
{
    public class TransactionViewModel
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
        
        public bool IsPending { get; set; }
        
        public string? Notes { get; set; }
        
        public List<TagViewModel> Tags { get; set; } = new();
        
        public List<AttachmentViewModel> Attachments { get; set; } = new();
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
    
    public class TransactionFilterModel
    {
        public DateTimeRange DateRange { get; set; } = DateTimeRange.CurrentMonth();
        
        public List<string> AccountIds { get; set; } = new();
        
        public List<string> CategoryIds { get; set; } = new();
        
        public List<TransactionType> Types { get; set; } = new();
        
        public decimal? MinAmount { get; set; }
        
        public decimal? MaxAmount { get; set; }
        
        public string? SearchTerm { get; set; }
        
        public bool IncludePending { get; set; } = true;
        
        public string SortBy { get; set; } = "Date";
        
        public bool SortAscending { get; set; } = false;
        
        public int Page { get; set; } = 1;
        
        public int PageSize { get; set; } = 20;
    }
}