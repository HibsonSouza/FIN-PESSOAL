namespace FinanceManager.ClientApp.Models
{
    public enum TransactionType
    {
        Income,
        Expense,
        Transfer
    }
    
    public static class TransactionTypeConstants
    {
        public static readonly TransactionType INCOME = TransactionType.Income;
        public static readonly TransactionType EXPENSE = TransactionType.Expense;
        public static readonly TransactionType TRANSFER = TransactionType.Transfer;
        public static readonly TransactionType INVESTMENT = TransactionType.Income; // Usando Income como fallback
    }
}