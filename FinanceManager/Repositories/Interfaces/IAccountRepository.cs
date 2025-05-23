using FinanceManager.Models;
using FinanceManager.Models.Enums;

namespace FinanceManager.Repositories.Interfaces
{
    /// <summary>
    /// Interface de repositório para a entidade Account
    /// </summary>
    public interface IAccountRepository : IRepository<Account>
    {
        Task<IEnumerable<Account>> GetByUserIdAsync(int userId);
        Task<decimal> GetTotalBalanceAsync(int userId);
        Task<IEnumerable<Account>> GetByTypeAsync(int userId, AccountType type);
    }
}
