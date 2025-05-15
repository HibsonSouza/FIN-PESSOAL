using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FinanceManager.ClientApp.Models;
using FinanceManager.ClientApp.Services.Interfaces;

namespace FinanceManager.ClientApp.Services
{
    public class CreditCardService : ICreditCardService
    {
        private readonly HttpClient _httpClient;

        public CreditCardService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CreditCardViewModel>> GetCreditCardsAsync()
        {
            try
            {
                var creditCards = await _httpClient.GetFromJsonAsync<List<CreditCardViewModel>>("api/credit-cards");
                return creditCards ?? new List<CreditCardViewModel>();
            }
            catch (Exception)
            {
                // Em uma aplicação real, você deve registrar o erro
                return new List<CreditCardViewModel>();
            }
        }

        public async Task<CreditCardViewModel> GetCreditCardByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<CreditCardViewModel>($"api/credit-cards/{id}");
            }
            catch (Exception)
            {
                // Em uma aplicação real, você deve registrar o erro
                return null;
            }
        }

        public async Task<CreditCardViewModel> CreateCreditCardAsync(CreditCardCreateModel creditCard)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/credit-cards", creditCard);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<CreditCardViewModel>();
            }
            catch (Exception)
            {
                // Em uma aplicação real, você deve registrar o erro
                return null;
            }
        }

        public async Task<CreditCardViewModel> UpdateCreditCardAsync(int id, CreditCardUpdateModel creditCard)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/credit-cards/{id}", creditCard);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<CreditCardViewModel>();
            }
            catch (Exception)
            {
                // Em uma aplicação real, você deve registrar o erro
                return null;
            }
        }

        public async Task<bool> DeleteCreditCardAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/credit-cards/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                // Em uma aplicação real, você deve registrar o erro
                return false;
            }
        }

        public async Task<CreditCardStatementModel> GetCurrentStatementAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<CreditCardStatementModel>($"api/credit-cards/{id}/current-statement");
            }
            catch (Exception)
            {
                // Em uma aplicação real, você deve registrar o erro
                return null;
            }
        }

        public async Task<List<CreditCardStatementViewModel>> GetStatementsHistoryAsync(int id, int count = 6)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<CreditCardStatementViewModel>>(
                    $"api/credit-cards/{id}/statements?count={count}");
            }
            catch (Exception)
            {
                // Em uma aplicação real, você deve registrar o erro
                return new List<CreditCardStatementViewModel>();
            }
        }

        public async Task<List<TransactionViewModel>> GetCreditCardTransactionsAsync(int id, DateTime? startDate = null, DateTime? endDate = null)
        {
            try
            {
                string url = $"api/credit-cards/{id}/transactions?";
                
                if (startDate.HasValue)
                    url += $"startDate={startDate:yyyy-MM-dd}&";
                
                if (endDate.HasValue)
                    url += $"endDate={endDate:yyyy-MM-dd}";
                
                return await _httpClient.GetFromJsonAsync<List<TransactionViewModel>>(url);
            }
            catch (Exception)
            {
                // Em uma aplicação real, você deve registrar o erro
                return new List<TransactionViewModel>();
            }
        }
    }
}