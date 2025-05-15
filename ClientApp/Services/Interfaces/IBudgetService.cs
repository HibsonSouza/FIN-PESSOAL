using FinanceManager.ClientApp.Models;

namespace FinanceManager.ClientApp.Services.Interfaces
{
    public interface IBudgetService
    {
        Task<List<BudgetViewModel>> GetBudgets();
        Task<BudgetViewModel> GetBudgetById(string id);
        Task<bool> CreateBudget(BudgetCreateModel budget);
        Task<bool> UpdateBudget(string id, BudgetUpdateModel budget);
        Task<bool> DeleteBudget(string id);
        Task<List<BudgetProgressViewModel>> GetCurrentBudgetProgress();
    }
}