using FinanceManager.ClientApp.Models;

namespace FinanceManager.ClientApp.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryViewModel>> GetCategories();
        Task<CategoryViewModel> GetCategoryById(string id);
        Task<bool> CreateCategory(CategoryCreateModel category);
        Task<bool> UpdateCategory(string id, CategoryUpdateModel category);
        Task<bool> DeleteCategory(string id);
        Task<List<CategorySummaryModel>> GetCategorySummary(DateTimeRange dateRange);
    }
}