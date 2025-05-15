using System.Collections.Generic;
using System.Threading.Tasks;
using FinanceManager.ClientApp.Models;

namespace FinanceManager.ClientApp.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryViewModel>> GetCategoriesAsync(CategoryType? type = null);
        Task<CategoryViewModel> GetCategoryByIdAsync(int id);
        Task<CategoryViewModel> CreateCategoryAsync(CategoryCreateModel category);
        Task<CategoryViewModel> UpdateCategoryAsync(int id, CategoryUpdateModel category);
        Task<bool> DeleteCategoryAsync(int id);
        Task<List<CategoryViewModel>> GetParentCategoriesAsync();
        Task<List<CategoryViewModel>> GetSubcategoriesAsync(int parentId);
        Task<Dictionary<string, decimal>> GetCategorySpendingAsync(int month, int year);
    }
}