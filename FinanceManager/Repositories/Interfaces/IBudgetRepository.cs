using FinanceManager.Models;

namespace FinanceManager.Repositories.Interfaces
{
    /// <summary>
    /// Interface de repositório para a entidade Budget
    /// </summary>
    public interface IBudgetRepository : IRepository<Budget>
    {
        Task<IEnumerable<Budget>> GetByUserIdAsync(int userId);
        Task<IEnumerable<Budget>> GetActiveBudgetsAsync(int userId);
        Task<Budget?> GetByCategoryIdAndMonthAsync(int categoryId, int month, int year);
        Task<IEnumerable<Budget>> GetByMonthAsync(int userId, int month, int year);
    }
}
