using System.Collections.Generic;
using System.Threading.Tasks;
using FinanceManager.ClientApp.Models;

namespace FinanceManager.ClientApp.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryViewModel>> GetCategoriesAsync();
        Task<CategoryViewModel> GetCategoryByIdAsync(string id);
        Task<CategoryViewModel> CreateCategoryAsync(CategoryFormModel category);
        Task<CategoryViewModel> UpdateCategoryAsync(string id, CategoryFormModel category);
        Task DeleteCategoryAsync(string id);
        Task<List<CategoryViewModel>> GetCategoriesByTypeAsync(TransactionType type);
        Task InitializeDefaultCategoriesAsync();
    }

    public class CategoryService : ICategoryService
    {
        private readonly IHttpService _httpService;

        public CategoryService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<List<CategoryViewModel>> GetCategoriesAsync()
        {
            return await _httpService.GetAsync<List<CategoryViewModel>>("api/categories");
        }

        public async Task<CategoryViewModel> GetCategoryByIdAsync(string id)
        {
            return await _httpService.GetAsync<CategoryViewModel>($"api/categories/{id}");
        }

        public async Task<CategoryViewModel> CreateCategoryAsync(CategoryFormModel category)
        {
            return await _httpService.PostAsync<CategoryViewModel>("api/categories", category);
        }

        public async Task<CategoryViewModel> UpdateCategoryAsync(string id, CategoryFormModel category)
        {
            return await _httpService.PutAsync<CategoryViewModel>($"api/categories/{id}", category);
        }

        public async Task DeleteCategoryAsync(string id)
        {
            await _httpService.DeleteAsync($"api/categories/{id}");
        }

        public async Task<List<CategoryViewModel>> GetCategoriesByTypeAsync(TransactionType type)
        {
            return await _httpService.GetAsync<List<CategoryViewModel>>($"api/categories/by-type/{type}");
        }

        public async Task InitializeDefaultCategoriesAsync()
        {
            await _httpService.PostAsync<object>("api/categories/initialize-default", null);
        }
    }
}
