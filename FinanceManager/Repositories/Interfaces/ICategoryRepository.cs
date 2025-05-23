using FinanceManager.Models;
using FinanceManager.Models.Enums;

namespace FinanceManager.Repositories.Interfaces
{
    /// <summary>
    /// Interface de repositório para a entidade Category
    /// </summary>
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetByTypeAsync(TransactionType type);
        Task<IEnumerable<Category>> GetByUserIdAsync(int userId);
        Task<IEnumerable<Category>> GetDefaultCategoriesAsync();
    }
}
