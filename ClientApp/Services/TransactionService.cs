using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinanceManager.ClientApp.Models;

namespace FinanceManager.ClientApp.Services
{
    public interface ITransactionService
    {
        Task<List<TransactionViewModel>> GetTransactionsAsync(TransactionFilterModel filter = null);
        Task<TransactionViewModel> GetTransactionByIdAsync(string id);
        Task<TransactionViewModel> CreateTransactionAsync(TransactionFormModel transaction);
        Task<TransactionViewModel> UpdateTransactionAsync(string id, TransactionFormModel transaction);
        Task DeleteTransactionAsync(string id);
        Task<List<TransactionViewModel>> ImportTransactionsAsync(List<TransactionFormModel> transactions);
        Task<List<TransactionViewModel>> GetRecentTransactionsAsync(int count = 10);
    }

    public class TransactionService : ITransactionService
    {
        private readonly IHttpService _httpService;

        public TransactionService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<List<TransactionViewModel>> GetTransactionsAsync(TransactionFilterModel filter = null)
        {
            string queryString = "";
            
            if (filter != null)
            {
                var queryParams = new List<string>();
                
                if (filter.StartDate.HasValue)
                    queryParams.Add($"startDate={filter.StartDate.Value:yyyy-MM-dd}");
                    
                if (filter.EndDate.HasValue)
                    queryParams.Add($"endDate={filter.EndDate.Value:yyyy-MM-dd}");
                    
                if (filter.Type.HasValue)
                    queryParams.Add($"type={filter.Type.Value}");
                    
                if (!string.IsNullOrEmpty(filter.CategoryId))
                    queryParams.Add($"categoryId={filter.CategoryId}");
                    
                if (!string.IsNullOrEmpty(filter.AccountId))
                    queryParams.Add($"accountId={filter.AccountId}");
                    
                if (!string.IsNullOrEmpty(filter.CreditCardId))
                    queryParams.Add($"creditCardId={filter.CreditCardId}");
                    
                if (filter.MinAmount.HasValue)
                    queryParams.Add($"minAmount={filter.MinAmount.Value}");
                    
                if (filter.MaxAmount.HasValue)
                    queryParams.Add($"maxAmount={filter.MaxAmount.Value}");
                    
                if (!string.IsNullOrEmpty(filter.SearchTerm))
                    queryParams.Add($"searchTerm={Uri.EscapeDataString(filter.SearchTerm)}");
                    
                if (filter.IsReconciled.HasValue)
                    queryParams.Add($"isReconciled={filter.IsReconciled.Value}");
                    
                queryParams.Add($"page={filter.Page}");
                queryParams.Add($"pageSize={filter.PageSize}");
                
                queryString = string.Join("&", queryParams);
            }
            
            string url = "api/transactions";
            if (!string.IsNullOrEmpty(queryString))
            {
                url += $"?{queryString}";
            }
            
            return await _httpService.GetAsync<List<TransactionViewModel>>(url);
        }

        public async Task<TransactionViewModel> GetTransactionByIdAsync(string id)
        {
            return await _httpService.GetAsync<TransactionViewModel>($"api/transactions/{id}");
        }

        public async Task<TransactionViewModel> CreateTransactionAsync(TransactionFormModel transaction)
        {
            return await _httpService.PostAsync<TransactionViewModel>("api/transactions", transaction);
        }

        public async Task<TransactionViewModel> UpdateTransactionAsync(string id, TransactionFormModel transaction)
        {
            return await _httpService.PutAsync<TransactionViewModel>($"api/transactions/{id}", transaction);
        }

        public async Task DeleteTransactionAsync(string id)
        {
            await _httpService.DeleteAsync($"api/transactions/{id}");
        }

        public async Task<List<TransactionViewModel>> ImportTransactionsAsync(List<TransactionFormModel> transactions)
        {
            return await _httpService.PostAsync<List<TransactionViewModel>>("api/transactions/import", transactions);
        }

        public async Task<List<TransactionViewModel>> GetRecentTransactionsAsync(int count = 10)
        {
            return await _httpService.GetAsync<List<TransactionViewModel>>($"api/transactions/recent?count={count}");
        }
    }
}
