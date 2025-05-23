using FinanceManager.ClientApp.Models;
using FinanceManager.ClientApp.Services.Interfaces;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace FinanceManager.ClientApp.Services
{    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiEndpoint = "api/accounts";
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public AccountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<AccountViewModel>> GetAccountsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(_apiEndpoint);
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<AccountViewModel>>(_jsonOptions) 
                        ?? new List<AccountViewModel>();
                }
                
                return new List<AccountViewModel>();
            }
            catch
            {
                return new List<AccountViewModel>();
            }
        }

        public async Task<AccountViewModel> GetAccountByIdAsync(string id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiEndpoint}/{id}");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<AccountViewModel>(_jsonOptions) 
                        ?? new AccountViewModel();
                }
                
                return new AccountViewModel();
            }
            catch
            {
                return new AccountViewModel();
            }
        }

        public async Task<AccountViewModel> CreateAccountAsync(AccountFormModel model)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(_apiEndpoint, model);
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<AccountViewModel>(_jsonOptions) 
                        ?? new AccountViewModel();
                }
                
                return new AccountViewModel();
            }
            catch
            {
                return new AccountViewModel();
            }
        }

        public async Task<AccountViewModel> UpdateAccountAsync(AccountFormModel model)
        {
            try
            {
                string id = model.Id ?? string.Empty;
                var response = await _httpClient.PutAsJsonAsync($"{_apiEndpoint}/{id}", model);
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<AccountViewModel>(_jsonOptions) 
                        ?? new AccountViewModel();
                }
                
                return new AccountViewModel();
            }
            catch
            {
                return new AccountViewModel();
            }
        }

        public async Task<bool> DeleteAccountAsync(string id)
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
        
        public async Task<IEnumerable<TransactionViewModel>> GetAccountTransactionsAsync(string accountId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiEndpoint}/{accountId}/transactions");
                
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

        public async Task<decimal> GetTotalBalanceAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiEndpoint}/total-balance");
                
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<decimal>(_jsonOptions);
                    return result;
                }
                
                return 0;
            }
            catch
            {
                return 0;
            }
        }
    }
}