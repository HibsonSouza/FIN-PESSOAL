using FinanceManager.Models;
using FinanceManager.Repositories.Interfaces;

namespace FinanceManager.Services.Interfaces
{
    /// <summary>
    /// Interface para o serviço de metas financeiras
    /// </summary>
    public interface IGoalService
    {
        Task<IEnumerable<Goal>> GetAllGoalsAsync(int userId);
        Task<IEnumerable<Goal>> GetActiveGoalsAsync(int userId);
        Task<IEnumerable<Goal>> GetCompletedGoalsAsync(int userId);
        Task<Goal?> GetGoalByIdAsync(int id);
        Task<Goal> CreateGoalAsync(Goal goal);
        Task<Goal> UpdateGoalAsync(Goal goal);
        Task<Goal> AddContributionAsync(int goalId, decimal amount);
        Task DeleteGoalAsync(int id);
    }
}

namespace FinanceManager.Services
{
    /// <summary>
    /// Implementação do serviço de metas financeiras
    /// </summary>
    public class GoalService : IGoalService
    {
        private readonly IGoalRepository _goalRepository;

        public GoalService(IGoalRepository goalRepository)
        {
            _goalRepository = goalRepository;
        }

        public async Task<IEnumerable<Goal>> GetAllGoalsAsync(int userId)
        {
            return await _goalRepository.GetByUserIdAsync(userId);
        }

        public async Task<IEnumerable<Goal>> GetActiveGoalsAsync(int userId)
        {
            return await _goalRepository.GetActiveGoalsAsync(userId);
        }

        public async Task<IEnumerable<Goal>> GetCompletedGoalsAsync(int userId)
        {
            return await _goalRepository.GetCompletedGoalsAsync(userId);
        }

        public async Task<Goal?> GetGoalByIdAsync(int id)
        {
            return await _goalRepository.GetByIdAsync(id);
        }

        public async Task<Goal> CreateGoalAsync(Goal goal)
        {
            goal.CreatedAt = DateTime.UtcNow;
            goal.UpdatedAt = DateTime.UtcNow;
            
            return await _goalRepository.CreateAsync(goal);
        }

        public async Task<Goal> UpdateGoalAsync(Goal goal)
        {
            goal.UpdatedAt = DateTime.UtcNow;
            
            return await _goalRepository.UpdateAsync(goal);
        }

        public async Task<Goal> AddContributionAsync(int goalId, decimal amount)
        {
            var goal = await _goalRepository.GetByIdAsync(goalId);
            if (goal == null)
            {
                throw new KeyNotFoundException($"Meta com ID {goalId} não encontrada.");
            }
            
            goal.CurrentAmount += amount;
            goal.UpdatedAt = DateTime.UtcNow;
            
            return await _goalRepository.UpdateAsync(goal);
        }

        public async Task DeleteGoalAsync(int id)
        {
            var goal = await _goalRepository.GetByIdAsync(id);
            if (goal != null)
            {
                await _goalRepository.DeleteAsync(goal);
            }
        }
    }
}
