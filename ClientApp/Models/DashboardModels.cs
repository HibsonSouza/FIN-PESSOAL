namespace FinanceManager.ClientApp.Models
{
    public class AccountSummaryViewModel
    {
        public string Id { get; set; } = string.Empty;
        
        public string Name { get; set; } = string.Empty;
        
        public string Type { get; set; } = string.Empty;
        
        public decimal Balance { get; set; }
        
        public string? Color { get; set; }
        
        public string BankName { get; set; } = string.Empty;
    }
    
    public class RecentTransactionModel
    {
        public string Id { get; set; } = string.Empty;
        
        public string Description { get; set; } = string.Empty;
        
        public decimal Amount { get; set; }
        
        public DateTime Date { get; set; }
        
        public TransactionType Type { get; set; }
        
        public string? CategoryName { get; set; }
        
        public string? AccountName { get; set; }
    }
    
    public class CategorySummary
    {
        public string Id { get; set; } = string.Empty;
        
        public string Name { get; set; } = string.Empty;
        
        public decimal Amount { get; set; }
        
        public double Percentage { get; set; }
        
        public string? Color { get; set; }
        
        public TransactionType Type { get; set; }
    }
    
    public class BalanceForecast
    {
        public DateTime Date { get; set; }
        
        public decimal ProjectedBalance { get; set; }
        
        public decimal ActualBalance { get; set; }
        
        public decimal ProjectedIncome { get; set; }
        
        public decimal ProjectedExpenses { get; set; }
    }
    
    public class SavingsGoalProgress
    {
        public string Id { get; set; } = string.Empty;
        
        public string Name { get; set; } = string.Empty;
        
        public decimal TargetAmount { get; set; }
        
        public decimal CurrentAmount { get; set; }
        
        public double PercentComplete { get; set; }
        
        public DateTime? TargetDate { get; set; }
        
        public string? Color { get; set; }
    }
}