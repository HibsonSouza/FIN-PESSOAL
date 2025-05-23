using FinanceManager.ClientApp.Models;
using FinanceManager.ClientApp.Services.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

namespace FinanceManager.ClientApp.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiEndpoint = "api/dashboard";
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public DashboardService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<DashboardViewModel?> GetDashboardDataAsync(FinanceManager.ClientApp.Models.DateTimeRange dateRange)
        {
            try
            {
                var queryString = $"?startDate={dateRange.Start:yyyy-MM-dd}&endDate={dateRange.End:yyyy-MM-dd}";
                var response = await _httpClient.GetAsync($"{_apiEndpoint}{queryString}");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<DashboardViewModel>(_jsonOptions);
                }
                
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter dados do dashboard: {ex.Message}");
                return null;
            }
        }

        public async Task<decimal> GetTotalBalanceAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiEndpoint}/total-balance");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<decimal>(_jsonOptions);
                }
                
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<decimal> GetMonthlyIncomeAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiEndpoint}/monthly-income");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<decimal>(_jsonOptions);
                }
                
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<decimal> GetMonthlyExpensesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiEndpoint}/monthly-expenses");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<decimal>(_jsonOptions);
                }
                
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<IEnumerable<CategorySummaryViewModel>> GetExpensesByCategoryAsync(FinanceManager.ClientApp.Models.DateTimeRange dateRange)
        {
            try
            {
                var queryString = $"?startDate={dateRange.Start:yyyy-MM-dd}&endDate={dateRange.End:yyyy-MM-dd}";
                var response = await _httpClient.GetAsync($"{_apiEndpoint}/expenses-by-category{queryString}");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<CategorySummaryViewModel>>(_jsonOptions) 
                        ?? new List<CategorySummaryViewModel>();
                }
                
                return new List<CategorySummaryViewModel>();
            }
            catch
            {
                return new List<CategorySummaryViewModel>();
            }
        }

        public async Task<IEnumerable<BalanceForecastViewModel>> GetCashFlowAsync(FinanceManager.ClientApp.Models.DateTimeRange dateRange)
        {
            try
            {
                var queryString = $"?startDate={dateRange.Start:yyyy-MM-dd}&endDate={dateRange.End:yyyy-MM-dd}";
                var response = await _httpClient.GetAsync($"{_apiEndpoint}/cash-flow{queryString}");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<BalanceForecastViewModel>>(_jsonOptions) 
                        ?? new List<BalanceForecastViewModel>();
                }
                
                return new List<BalanceForecastViewModel>();
            }
            catch
            {
                return new List<BalanceForecastViewModel>();
            }
        }

        public async Task<IEnumerable<SavingsGoalProgress>> GetSavingsGoalsProgressAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiEndpoint}/savings-goals-progress");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<SavingsGoalProgress>>(_jsonOptions) 
                        ?? new List<SavingsGoalProgress>();
                }
                
                return new List<SavingsGoalProgress>();
            }
            catch
            {
                return new List<SavingsGoalProgress>();
            }
        }

        public async Task<List<BudgetProgressViewModel>> GetBudgetProgressAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiEndpoint}/budget-progress");
                
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