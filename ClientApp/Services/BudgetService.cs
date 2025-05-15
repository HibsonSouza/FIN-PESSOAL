using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FinanceManager.ClientApp.Models;
using FinanceManager.ClientApp.Services.Interfaces;

namespace FinanceManager.ClientApp.Services
{
    public class BudgetService : IBudgetService
    {
        private readonly HttpClient _httpClient;

        public BudgetService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<BudgetViewModel>> GetBudgetsAsync()
        {
            try
            {
                var budgets = await _httpClient.GetFromJsonAsync<List<BudgetViewModel>>("api/budgets");
                return budgets ?? new List<BudgetViewModel>();
            }
            catch (Exception)
            {
                // Em uma aplicação real, você deve registrar o erro
                return new List<BudgetViewModel>();
            }
        }

        public async Task<BudgetViewModel> GetBudgetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<BudgetViewModel>($"api/budgets/{id}");
            }
            catch (Exception)
            {
                // Em uma aplicação real, você deve registrar o erro
                return null;
            }
        }

        public async Task<List<BudgetViewModel>> GetActiveBudgetsAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<BudgetViewModel>>("api/budgets/active");
            }
            catch (Exception)
            {
                // Em uma aplicação real, você deve registrar o erro
                return new List<BudgetViewModel>();
            }
        }

        public async Task<List<BudgetViewModel>> GetBudgetsByPeriodAsync(BudgetPeriod period)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<BudgetViewModel>>($"api/budgets/period/{period}");
            }
            catch (Exception)
            {
                // Em uma aplicação real, você deve registrar o erro
                return new List<BudgetViewModel>();
            }
        }

        public async Task<BudgetViewModel> CreateBudgetAsync(BudgetCreateModel budget)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/budgets", budget);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<BudgetViewModel>();
            }
            catch (Exception)
            {
                // Em uma aplicação real, você deve registrar o erro
                return null;
            }
        }

        public async Task<BudgetViewModel> UpdateBudgetAsync(int id, BudgetUpdateModel budget)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/budgets/{id}", budget);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<BudgetViewModel>();
            }
            catch (Exception)
            {
                // Em uma aplicação real, você deve registrar o erro
                return null;
            }
        }

        public async Task<bool> DeleteBudgetAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/budgets/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                // Em uma aplicação real, você deve registrar o erro
                return false;
            }
        }

        public async Task<Dictionary<string, decimal>> GetCategoryBudgetSummaryAsync(int month, int year)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Dictionary<string, decimal>>(
                    $"api/budgets/category-summary?month={month}&year={year}");
            }
            catch (Exception)
            {
                // Em uma aplicação real, você deve registrar o erro
                return new Dictionary<string, decimal>();
            }
        }
    }
}