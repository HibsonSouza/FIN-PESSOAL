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
        }        public async Task<List<TransactionViewModel>> GetTransactionsAsync() // Renomeado e tipo de retorno atualizado
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

        public async Task<List<TransactionViewModel>> GetTransactionsByDateRange(DateTimeRange dateRange) // Renomeado
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
        }        public async Task<List<TransactionViewModel>> GetRecentTransactions(int count) // Renomeado
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

        public async Task<TransactionViewModel?> GetTransactionByIdAsync(string id) // Renomeado e tipo de retorno atualizado para nullable
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiEndpoint}/{id}");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<TransactionViewModel>(_jsonOptions);
                }
                
                return null; // Retorna null se não for bem-sucedido ou não encontrado
            }
            catch
            {
                return null; // Retorna null em caso de exceção
            }
        }        public async Task<TransactionViewModel?> CreateTransactionAsync(TransactionCreateModel transaction) // Renomeado e tipo de retorno atualizado para nullable
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(_apiEndpoint, transaction);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<TransactionViewModel>(_jsonOptions);
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<TransactionViewModel?> CreateTransactionAsync(TransactionFormModel transaction)
        {
            try
            {
                // Converter do modelo de formulário para o modelo de criação
                var createModel = new TransactionCreateModel
                {
                    Description = transaction.Description,
                    Amount = transaction.Amount,
                    Date = transaction.Date,
                    Type = transaction.Type,
                    AccountId = transaction.AccountId ?? string.Empty,
                    CategoryId = transaction.CategoryId,
                    ToAccountId = transaction.ToAccountId,
                    IsRecurring = transaction.IsRecurring,
                    RecurrencePattern = transaction.RecurrencePattern,
                    EndDate = transaction.EndDate,
                    IsPending = transaction.IsPending,
                    Notes = transaction.Notes,
                    TagIds = transaction.TagIds?.ToList() ?? new List<string>()
                };
                
                return await CreateTransactionAsync(createModel);
            }
            catch
            {
                return null;
            }
        }

        public async Task<TransactionViewModel?> UpdateTransactionAsync(string id, TransactionUpdateModel transaction)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_apiEndpoint}/{id}", transaction);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<TransactionViewModel>(_jsonOptions);
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

    public async Task<TransactionViewModel?> UpdateTransactionAsync(string id, TransactionFormModel transaction)
    {
        try
        {
            // Converter do modelo de formulário para o modelo de atualização
            var updateModel = new TransactionUpdateModel
            {
                Description = transaction.Description,
                Amount = transaction.Amount,
                Date = transaction.Date,
                Type = transaction.Type,
                AccountId = transaction.AccountId ?? string.Empty,
                CategoryId = transaction.CategoryId,
                ToAccountId = transaction.ToAccountId,
                IsPending = transaction.IsPending,
                Notes = transaction.Notes,
                TagIds = transaction.TagIds?.ToList() ?? new List<string>(),
                UpdateRecurringSeries = transaction.IsRecurring, // Usar IsRecurring como indicador de atualização da série recorrente
                Location = transaction.Location,
                IsReconciled = transaction.IsReconciled,
                CreditCardId = transaction.CreditCardId
            };
            
            return await UpdateTransactionAsync(id, updateModel);
        }
        catch
        {
            return null;
        }
    }

        public async Task<bool> DeleteTransactionAsync(string id) // Mantido, já estava correto
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
          // Método para buscar transações com filtro, conforme a interface
        public async Task<List<TransactionViewModel>> GetFilteredTransactionsAsync(TransactionFilterModel filter)
        {
            try
            {
                // Constrói a query string dinamicamente
                var queryBuilder = new StringBuilder($"{_apiEndpoint}/filter?");
                if (filter.StartDate.HasValue)
                    queryBuilder.Append($"StartDate={filter.StartDate.Value:yyyy-MM-dd}&");
                if (filter.EndDate.HasValue)
                    queryBuilder.Append($"EndDate={filter.EndDate.Value:yyyy-MM-dd}&");
                if (!string.IsNullOrEmpty(filter.SearchTerm)) // Corrigido para SearchTerm
                    queryBuilder.Append($"SearchTerm={Uri.EscapeDataString(filter.SearchTerm)}&"); // Corrigido para SearchTerm
                if (filter.Type.HasValue)
                    queryBuilder.Append($"Type={filter.Type.Value}&");
                if (filter.IsReconciled.HasValue)
                    queryBuilder.Append($"IsReconciled={filter.IsReconciled.Value}&");

                if (filter.AccountIds != null && filter.AccountIds.Any())
                {
                    foreach (var accountId in filter.AccountIds)
                    {
                        queryBuilder.Append($"AccountIds={accountId}&");
                    }
                }
                if (filter.CategoryIds != null && filter.CategoryIds.Any())
                {
                    foreach (var categoryId in filter.CategoryIds)
                    {
                        queryBuilder.Append($"CategoryIds={categoryId}&");
                    }
                }
                if (filter.TagIds != null && filter.TagIds.Any())
                {
                    foreach (var tagId in filter.TagIds)
                    {
                        queryBuilder.Append($"TagIds={tagId}&");
                    }
                }
                
                var queryString = queryBuilder.ToString().TrimEnd('&');
                var response = await _httpClient.GetAsync(queryString);

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
    }
}