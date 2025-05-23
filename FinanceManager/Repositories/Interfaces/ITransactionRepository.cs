using FinanceManager.Models;
using FinanceManager.Models.Enums;

namespace FinanceManager.Repositories.Interfaces
{
    /// <summary>
    /// Interface de repositório para a entidade Transaction
    /// </summary>
    public interface ITransactionRepository : IRepository<Transaction>
    {
        Task<IEnumerable<Transaction>> GetByUserIdAsync(int userId);
        Task<IEnumerable<Transaction>> GetByAccountIdAsync(int accountId);
        Task<IEnumerable<Transaction>> GetByCategoryIdAsync(int categoryId);
        Task<IEnumerable<Transaction>> GetByTypeAsync(int userId, TransactionType type);
        Task<IEnumerable<Transaction>> GetByDateRangeAsync(int userId, DateTime startDate, DateTime endDate);
        Task<IEnumerable<Transaction>> GetFilteredTransactionsAsync(
            int userId, 
            DateTime? startDate, 
            DateTime? endDate, 
            int? accountId, 
            int? categoryId, 
            TransactionType? type, 
            string? searchTerm);
        Task<decimal> GetTotalByTypeAndPeriodAsync(int userId, TransactionType type, DateTime startDate, DateTime endDate);
        Task<IEnumerable<Transaction>> GetRecentTransactionsAsync(int userId, int count);
    }
}
