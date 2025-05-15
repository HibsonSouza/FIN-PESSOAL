using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinanceManager.ClientApp.Models;

namespace FinanceManager.ClientApp.Services
{
    public interface IAccountService
    {
        Task<List<AccountViewModel>> GetAccountsAsync();
        Task<AccountViewModel> GetAccountByIdAsync(string id);
        Task<AccountViewModel> CreateAccountAsync(AccountFormModel account);
        Task<AccountViewModel> UpdateAccountAsync(string id, AccountFormModel account);
        Task DeleteAccountAsync(string id);
        Task<List<TransactionViewModel>> GetAccountTransactionsAsync(string id, DateTime? startDate = null, DateTime? endDate = null);
    }

    public class AccountService : IAccountService
    {
        private readonly IHttpService _httpService;

        public AccountService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<List<AccountViewModel>> GetAccountsAsync()
        {
            return await _httpService.GetAsync<List<AccountViewModel>>("api/accounts");
        }

        public async Task<AccountViewModel> GetAccountByIdAsync(string id)
        {
            return await _httpService.GetAsync<AccountViewModel>($"api/accounts/{id}");
        }

        public async Task<AccountViewModel> CreateAccountAsync(AccountFormModel account)
        {
            return await _httpService.PostAsync<AccountViewModel>("api/accounts", account);
        }

        public async Task<AccountViewModel> UpdateAccountAsync(string id, AccountFormModel account)
        {
            return await _httpService.PutAsync<AccountViewModel>($"api/accounts/{id}", account);
        }

        public async Task DeleteAccountAsync(string id)
        {
            await _httpService.DeleteAsync($"api/accounts/{id}");
        }

        public async Task<List<TransactionViewModel>> GetAccountTransactionsAsync(string id, DateTime? startDate = null, DateTime? endDate = null)
        {
            string queryString = "";
            
            if (startDate.HasValue)
            {
                queryString += $"startDate={startDate.Value:yyyy-MM-dd}";
            }
            
            if (endDate.HasValue)
            {
                queryString += (queryString.Length > 0 ? "&" : "") + $"endDate={endDate.Value:yyyy-MM-dd}";
            }
            
            string url = $"api/accounts/{id}/transactions";
            if (!string.IsNullOrEmpty(queryString))
            {
                url += $"?{queryString}";
            }
            
            return await _httpService.GetAsync<List<TransactionViewModel>>(url);
        }
    }
}
