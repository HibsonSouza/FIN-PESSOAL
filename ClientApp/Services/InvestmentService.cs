using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FinanceManager.ClientApp.Models;
using FinanceManager.ClientApp.Services.Interfaces;

namespace FinanceManager.ClientApp.Services
{
    public class InvestmentService : IInvestmentService
    {
        private readonly HttpClient _httpClient;

        public InvestmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<InvestmentViewModel>> GetInvestmentsAsync()
        {
            try
            {
                var investments = await _httpClient.GetFromJsonAsync<List<InvestmentViewModel>>("api/investments");
                return investments ?? new List<InvestmentViewModel>();
            }
            catch (Exception)
            {
                // Em uma aplicação real, você deve registrar o erro
                return new List<InvestmentViewModel>();
            }
        }

        public async Task<InvestmentViewModel> GetInvestmentByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<InvestmentViewModel>($"api/investments/{id}");
            }
            catch (Exception)
            {
                // Em uma aplicação real, você deve registrar o erro
                return null;
            }
        }

        public async Task<InvestmentViewModel> CreateInvestmentAsync(InvestmentCreateModel investment)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/investments", investment);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<InvestmentViewModel>();
            }
            catch (Exception)
            {
                // Em uma aplicação real, você deve registrar o erro
                return null;
            }
        }

        public async Task<InvestmentViewModel> UpdateInvestmentAsync(int id, InvestmentUpdateModel investment)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/investments/{id}", investment);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<InvestmentViewModel>();
            }
            catch (Exception)
            {
                // Em uma aplicação real, você deve registrar o erro
                return null;
            }
        }

        public async Task<bool> DeleteInvestmentAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/investments/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                // Em uma aplicação real, você deve registrar o erro
                return false;
            }
        }

        public async Task<List<InvestmentTransactionViewModel>> GetInvestmentTransactionsAsync(int investmentId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<InvestmentTransactionViewModel>>(
                    $"api/investments/{investmentId}/transactions");
            }
            catch (Exception)
            {
                // Em uma aplicação real, você deve registrar o erro
                return new List<InvestmentTransactionViewModel>();
            }
        }

        public async Task<InvestmentTransactionViewModel> AddInvestmentTransactionAsync(
            int investmentId, InvestmentTransactionViewModel transaction)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(
                    $"api/investments/{investmentId}/transactions", transaction);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<InvestmentTransactionViewModel>();
            }
            catch (Exception)
            {
                // Em uma aplicação real, você deve registrar o erro
                return null;
            }
        }

        public async Task<Dictionary<DateTime, decimal>> GetInvestmentPerformanceAsync(
            int investmentId, DateTime startDate, DateTime endDate)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Dictionary<DateTime, decimal>>(
                    $"api/investments/{investmentId}/performance?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}");
            }
            catch (Exception)
            {
                // Em uma aplicação real, você deve registrar o erro
                return new Dictionary<DateTime, decimal>();
            }
        }

        public async Task<decimal> GetTotalInvestmentsValueAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<decimal>("api/investments/total-value");
            }
            catch (Exception)
            {
                // Em uma aplicação real, você deve registrar o erro
                return 0;
            }
        }
        
        public async Task<InvestmentTransactionViewModel> UpdateInvestmentTransactionAsync(string transactionId, InvestmentTransactionFormModel model)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/investment-transactions/{transactionId}", model);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<InvestmentTransactionViewModel>();
            }
            catch (Exception)
            {
                // Em uma aplicação real, você deve registrar o erro
                return null;
            }
        }
        
        public async Task<InvestmentTransactionViewModel> AddInvestmentTransactionAsync(InvestmentTransactionFormModel model)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/investment-transactions", model);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<InvestmentTransactionViewModel>();
            }
            catch (Exception)
            {
                // Em uma aplicação real, você deve registrar o erro
                return null;
            }
        }
    }
}