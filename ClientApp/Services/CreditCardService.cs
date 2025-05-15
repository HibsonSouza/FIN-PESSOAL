using FinanceManager.ClientApp.Models;
using FinanceManager.ClientApp.Services.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

namespace FinanceManager.ClientApp.Services
{
    public class CreditCardService : ICreditCardService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiEndpoint = "api/creditcards";
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public CreditCardService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CreditCardViewModel>> GetCreditCards()
        {
            try
            {
                var response = await _httpClient.GetAsync(_apiEndpoint);
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<CreditCardViewModel>>(_jsonOptions) 
                        ?? new List<CreditCardViewModel>();
                }
                
                return new List<CreditCardViewModel>();
            }
            catch
            {
                return new List<CreditCardViewModel>();
            }
        }

        public async Task<CreditCardViewModel> GetCreditCardById(string id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiEndpoint}/{id}");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<CreditCardViewModel>(_jsonOptions) 
                        ?? new CreditCardViewModel();
                }
                
                return new CreditCardViewModel();
            }
            catch
            {
                return new CreditCardViewModel();
            }
        }

        public async Task<bool> CreateCreditCard(CreditCardCreateModel creditCard)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(_apiEndpoint, creditCard);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateCreditCard(string id, CreditCardUpdateModel creditCard)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_apiEndpoint}/{id}", creditCard);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteCreditCard(string id)
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

        public async Task<List<CreditCardTransactionViewModel>> GetCreditCardTransactions(string creditCardId, DateTimeRange dateRange)
        {
            try
            {
                var response = await _httpClient.GetAsync(
                    $"{_apiEndpoint}/{creditCardId}/transactions?startDate={dateRange.Start:yyyy-MM-dd}&endDate={dateRange.End:yyyy-MM-dd}");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<CreditCardTransactionViewModel>>(_jsonOptions) 
                        ?? new List<CreditCardTransactionViewModel>();
                }
                
                return new List<CreditCardTransactionViewModel>();
            }
            catch
            {
                return new List<CreditCardTransactionViewModel>();
            }
        }

        public async Task<bool> AddCreditCardTransaction(string creditCardId, CreditCardTransactionCreateModel transaction)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_apiEndpoint}/{creditCardId}/transactions", transaction);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}