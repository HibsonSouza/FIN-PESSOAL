using FinanceManager.Models;

namespace FinanceManager.Repositories.Interfaces
{
    /// <summary>
    /// Interface de repositório para a entidade Goal
    /// </summary>
    public interface IGoalRepository : IRepository<Goal>
    {
        Task<IEnumerable<Goal>> GetByUserIdAsync(int userId);
        Task<IEnumerable<Goal>> GetActiveGoalsAsync(int userId);
        Task<IEnumerable<Goal>> GetCompletedGoalsAsync(int userId);
    }
}
