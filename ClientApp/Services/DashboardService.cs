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

        public async Task<decimal> GetTotalBalance()
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

        public async Task<decimal> GetMonthlyIncome()
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

        public async Task<decimal> GetMonthlyExpenses()
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

        public async Task<IEnumerable<ChartData>> GetCategoryExpenses(DateTimeRange dateRange)
        {
            try
            {
                var response = await _httpClient.GetAsync(
                    $"{_apiEndpoint}/category-expenses?startDate={dateRange.Start:yyyy-MM-dd}&endDate={dateRange.End:yyyy-MM-dd}");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<ChartData>>(_jsonOptions) 
                        ?? new List<ChartData>();
                }
                
                return new List<ChartData>();
            }
            catch
            {
                return new List<ChartData>();
            }
        }

        public async Task<IEnumerable<ChartData>> GetMonthlyBalanceHistory(int numberOfMonths = 6)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiEndpoint}/monthly-balance-history?months={numberOfMonths}");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<ChartData>>(_jsonOptions) 
                        ?? new List<ChartData>();
                }
                
                return new List<ChartData>();
            }
            catch
            {
                return new List<ChartData>();
            }
        }

        public async Task<IEnumerable<ChartData>> GetSavingsGoalsProgress()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiEndpoint}/savings-goals-progress");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<ChartData>>(_jsonOptions) 
                        ?? new List<ChartData>();
                }
                
                return new List<ChartData>();
            }
            catch
            {
                return new List<ChartData>();
            }
        }
    }
}