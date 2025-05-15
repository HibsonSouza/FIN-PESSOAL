using FinanceManager.ClientApp.Models;

namespace FinanceManager.ClientApp.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<List<TransactionViewModel>> GetTransactions();
        Task<List<TransactionViewModel>> GetTransactionsByDateRange(DateTimeRange dateRange);
        Task<List<TransactionViewModel>> GetRecentTransactions(int count);
        Task<TransactionViewModel> GetTransactionById(string id);
        Task<bool> CreateTransaction(TransactionCreateModel transaction);
        Task<bool> UpdateTransaction(string id, TransactionUpdateModel transaction);
        Task<bool> DeleteTransaction(string id);
        Task<bool> DeleteTransactionAsync(string id);
        Task<List<TransactionViewModel>> GetTransactionsAsync(TransactionFilterModel filter);
    }
}