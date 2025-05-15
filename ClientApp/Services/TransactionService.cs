using FinanceManager.ClientApp.Models;
using FinanceManager.ClientApp.Services.Interfaces;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace FinanceManager.ClientApp.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiEndpoint = "api/transactions";
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public TransactionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TransactionViewModel>> GetTransactions()
        {
            try
            {
                var response = await _httpClient.GetAsync(_apiEndpoint);
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<TransactionViewModel>>(_jsonOptions) 
                        ?? new List<TransactionViewModel>();
                }
                
                return new List<TransactionViewModel>();
            }
            catch
            {
                return new List<TransactionViewModel>();
            }
        }

        public async Task<List<TransactionViewModel>> GetTransactionsByDateRange(DateTimeRange dateRange)
        {
            try
            {
                var response = await _httpClient.GetAsync(
                    $"{_apiEndpoint}/range?startDate={dateRange.Start:yyyy-MM-dd}&endDate={dateRange.End:yyyy-MM-dd}");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<TransactionViewModel>>(_jsonOptions) 
                        ?? new List<TransactionViewModel>();
                }
                
                return new List<TransactionViewModel>();
            }
            catch
            {
                return new List<TransactionViewModel>();
            }
        }

        public async Task<List<TransactionViewModel>> GetRecentTransactions(int count)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiEndpoint}/recent?count={count}");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<TransactionViewModel>>(_jsonOptions) 
                        ?? new List<TransactionViewModel>();
                }
                
                return new List<TransactionViewModel>();
            }
            catch
            {
                return new List<TransactionViewModel>();
            }
        }

        public async Task<TransactionViewModel> GetTransactionById(string id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiEndpoint}/{id}");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<TransactionViewModel>(_jsonOptions) 
                        ?? new TransactionViewModel();
                }
                
                return new TransactionViewModel();
            }
            catch
            {
                return new TransactionViewModel();
            }
        }

        public async Task<bool> CreateTransaction(TransactionCreateModel transaction)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(_apiEndpoint, transaction);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateTransaction(string id, TransactionUpdateModel transaction)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_apiEndpoint}/{id}", transaction);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteTransaction(string id)
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
    }
}