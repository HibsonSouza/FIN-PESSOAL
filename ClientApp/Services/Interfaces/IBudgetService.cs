using System.Collections.Generic;
using System.Threading.Tasks;
using FinanceManager.ClientApp.Models;

namespace FinanceManager.ClientApp.Services.Interfaces
{
    public interface IBudgetService
    {
        Task<List<BudgetViewModel>> GetBudgetsAsync();
        Task<BudgetViewModel> GetBudgetByIdAsync(int id);
        Task<List<BudgetViewModel>> GetActiveBudgetsAsync();
        Task<List<BudgetViewModel>> GetBudgetsByPeriodAsync(BudgetPeriod period);
        Task<BudgetViewModel> CreateBudgetAsync(BudgetCreateModel budget);
        Task<BudgetViewModel> UpdateBudgetAsync(int id, BudgetUpdateModel budget);
        Task<bool> DeleteBudgetAsync(int id);
        Task<Dictionary<string, decimal>> GetCategoryBudgetSummaryAsync(int month, int year);
    }
}