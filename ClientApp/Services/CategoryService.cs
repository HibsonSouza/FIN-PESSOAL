using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FinanceManager.ClientApp.Models;
using FinanceManager.ClientApp.Services.Interfaces;

namespace FinanceManager.ClientApp.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CategoryViewModel>> GetCategoriesAsync(CategoryType? type = null)
        {
            try
            {
                string url = "api/categories";
                if (type.HasValue)
                {
                    url += $"?type={type}";
                }
                
                var categories = await _httpClient.GetFromJsonAsync<List<CategoryViewModel>>(url);
                return categories ?? new List<CategoryViewModel>();
            }
            catch (Exception)
            {
                // Em uma aplicação real, você deve registrar o erro
                return new List<CategoryViewModel>();
            }
        }

        public async Task<CategoryViewModel> GetCategoryByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<CategoryViewModel>($"api/categories/{id}");
            }
            catch (Exception)
            {
                // Em uma aplicação real, você deve registrar o erro
                return null;
            }
        }

        public async Task<CategoryViewModel> CreateCategoryAsync(CategoryCreateModel category)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/categories", category);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<CategoryViewModel>();
            }
            catch (Exception)
            {
                // Em uma aplicação real, você deve registrar o erro
                return null;
            }
        }

        public async Task<CategoryViewModel> UpdateCategoryAsync(int id, CategoryUpdateModel category)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/categories/{id}", category);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<CategoryViewModel>();
            }
            catch (Exception)
            {
                // Em uma aplicação real, você deve registrar o erro
                return null;
            }
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/categories/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                // Em uma aplicação real, você deve registrar o erro
                return false;
            }
        }

        public async Task<List<CategoryViewModel>> GetParentCategoriesAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<CategoryViewModel>>("api/categories/parents");
            }
            catch (Exception)
            {
                // Em uma aplicação real, você deve registrar o erro
                return new List<CategoryViewModel>();
            }
        }

        public async Task<List<CategoryViewModel>> GetSubcategoriesAsync(int parentId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<CategoryViewModel>>($"api/categories/{parentId}/subcategories");
            }
            catch (Exception)
            {
                // Em uma aplicação real, você deve registrar o erro
                return new List<CategoryViewModel>();
            }
        }

        public async Task<Dictionary<string, decimal>> GetCategorySpendingAsync(int month, int year)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Dictionary<string, decimal>>(
                    $"api/categories/spending?month={month}&year={year}");
            }
            catch (Exception)
            {
                // Em uma aplicação real, você deve registrar o erro
                return new Dictionary<string, decimal>();
            }
        }
    }
}