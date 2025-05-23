namespace FinanceManager.ClientApp.Models
{
    public class CategorySummary
    {
        public string CategoryId { get; set; } = string.Empty;
        
        public string CategoryName { get; set; } = string.Empty;
        
        public string? CategoryColor { get; set; }
        
        public decimal TotalAmount { get; set; }
        
        public double Percentage { get; set; }
        
        public TransactionType TransactionType { get; set; }
    }
}
