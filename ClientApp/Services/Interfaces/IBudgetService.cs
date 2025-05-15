using BlazorFinanceManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorFinanceManager.Services
{
    public interface IBudgetService
    {
        Task<List<BudgetViewModel>> GetBudgetsAsync();
        Task<List<BudgetViewModel>> GetActiveBudgetsAsync();
        Task<List<BudgetViewModel>> GetBudgetsByCategoryAsync(string categoryId);
        Task<BudgetViewModel> GetBudgetByIdAsync(string id);
        Task<BudgetViewModel> CreateBudgetAsync(BudgetCreateModel budget);
        Task<BudgetViewModel> UpdateBudgetAsync(string id, BudgetUpdateModel budget);
        Task<bool> DeleteBudgetAsync(string id);
    }
}