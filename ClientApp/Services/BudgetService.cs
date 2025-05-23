using FinanceManager.ClientApp.Models;
using FinanceManager.ClientApp.Services.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

namespace FinanceManager.ClientApp.Services
{
    public class BudgetService : IBudgetService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiEndpoint = "api/budgets";
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public BudgetService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<BudgetViewModel>> GetBudgets()
        {
            try
            {
                var response = await _httpClient.GetAsync(_apiEndpoint);
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<BudgetViewModel>>(_jsonOptions) 
                        ?? new List<BudgetViewModel>();
                }
                
                return new List<BudgetViewModel>();
            }
            catch
            {
                return new List<BudgetViewModel>();
            }
        }

        public async Task<BudgetViewModel> GetBudgetById(string id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiEndpoint}/{id}");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<BudgetViewModel>(_jsonOptions) 
                        ?? new BudgetViewModel();
                }
                
                return new BudgetViewModel();
            }
            catch
            {
                return new BudgetViewModel();
            }
        }

        public async Task<bool> CreateBudget(BudgetCreateModel budget)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(_apiEndpoint, budget);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateBudget(string id, BudgetUpdateModel budget)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_apiEndpoint}/{id}", budget);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteBudget(string id)
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
        }

        public async Task<List<BudgetProgressViewModel>> GetCurrentBudgetProgress()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiEndpoint}/progress");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<BudgetProgressViewModel>>(_jsonOptions) 
                        ?? new List<BudgetProgressViewModel>();
                }
                
                return new List<BudgetProgressViewModel>();
            }
            catch
            {
                return new List<BudgetProgressViewModel>();
            }
        }
    }
}