using FinanceManager.ClientApp.Models;
using FinanceManager.ClientApp.Services.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

namespace FinanceManager.ClientApp.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiEndpoint = "api/categories";
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CategoryViewModel>> GetCategories()
        {
            try
            {
                var response = await _httpClient.GetAsync(_apiEndpoint);
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<CategoryViewModel>>(_jsonOptions) 
                        ?? new List<CategoryViewModel>();
                }
                
                return new List<CategoryViewModel>();
            }
            catch
            {
                return new List<CategoryViewModel>();
            }
        }

        public async Task<CategoryViewModel> GetCategoryById(string id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiEndpoint}/{id}");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<CategoryViewModel>(_jsonOptions) 
                        ?? new CategoryViewModel();
                }
                
                return new CategoryViewModel();
            }
            catch
            {
                return new CategoryViewModel();
            }
        }

        public async Task<bool> CreateCategory(CategoryCreateModel category)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(_apiEndpoint, category);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateCategory(string id, CategoryUpdateModel category)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_apiEndpoint}/{id}", category);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteCategory(string id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_apiEndpoint}/{id}");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }        public async Task<List<CategorySummary>> GetCategorySummary(DateTimeRange dateRange)
        {
            try
            {
                var response = await _httpClient.GetAsync(
                    $"{_apiEndpoint}/summary?startDate={dateRange.Start:yyyy-MM-dd}&endDate={dateRange.End:yyyy-MM-dd}");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<CategorySummary>>(_jsonOptions) 
                        ?? new List<CategorySummary>();
                }
                
                return new List<CategorySummary>();
            }
            catch
            {
                return new List<CategorySummary>();
            }
        }
    }
}