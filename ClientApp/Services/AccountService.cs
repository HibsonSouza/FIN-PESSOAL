using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FinanceManager.ClientApp.Models;
using FinanceManager.ClientApp.Services.Interfaces;

namespace FinanceManager.ClientApp.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;

        public AccountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<AccountViewModel>> GetAccountsAsync()
        {
            try
            {
                var accounts = await _httpClient.GetFromJsonAsync<List<AccountViewModel>>("api/accounts");
                return accounts ?? new List<AccountViewModel>();
            }
            catch (Exception)
            {
                // Em uma aplicação real, você deve registrar o erro
                return new List<AccountViewModel>();
            }
        }

        public async Task<AccountViewModel> GetAccountByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<AccountViewModel>($"api/accounts/{id}");
            }
            catch (Exception)
            {
                // Em uma aplicação real, você deve registrar o erro
                return null;
            }
        }

        public async Task<AccountViewModel> CreateAccountAsync(AccountCreateModel account)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/accounts", account);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<AccountViewModel>();
            }
            catch (Exception)
            {
                // Em uma aplicação real, você deve registrar o erro
                return null;
            }
        }

        public async Task<AccountViewModel> UpdateAccountAsync(int id, AccountUpdateModel account)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/accounts/{id}", account);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<AccountViewModel>();
            }
            catch (Exception)
            {
                // Em uma aplicação real, você deve registrar o erro
                return null;
            }
        }

        public async Task<bool> DeleteAccountAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/accounts/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                // Em uma aplicação real, você deve registrar o erro
                return false;
            }
        }

        public async Task<List<AccountBalanceHistoryViewModel>> GetAccountBalanceHistoryAsync(int id, int months)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<AccountBalanceHistoryViewModel>>(
                    $"api/accounts/{id}/balance-history?months={months}");
            }
            catch (Exception)
            {
                // Em uma aplicação real, você deve registrar o erro
                return new List<AccountBalanceHistoryViewModel>();
            }
        }
    }
}