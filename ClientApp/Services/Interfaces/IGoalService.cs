using FinanceManager.ClientApp.Models;

namespace FinanceManager.ClientApp.Services.Interfaces
{
    public interface IGoalService
    {
        Task<List<GoalViewModel>> GetGoals();
        Task<GoalViewModel> GetGoalById(string id);
        Task<bool> CreateGoal(GoalCreateModel goal);
        Task<bool> UpdateGoal(string id, GoalUpdateModel goal);
        Task<bool> DeleteGoal(string id);
        Task<bool> AddContribution(string goalId, GoalContributionModel contribution);
    }
}