using FinanceManager.ClientApp.Models;

namespace FinanceManager.ClientApp.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<List<TransactionViewModel>> GetTransactionsAsync(); // Renomeado e alterado retorno
        Task<List<TransactionViewModel>> GetTransactionsByDateRange(DateTimeRange dateRange);
        Task<List<TransactionViewModel>> GetRecentTransactions(int count); // Adicionado Async e alterado retorno        Task<TransactionViewModel?> GetTransactionByIdAsync(string id); // Adicionado Async, alterado retorno para nullable
        Task<TransactionViewModel?> CreateTransactionAsync(TransactionCreateModel transaction); // Adicionado Async, alterado retorno para nullable
        Task<TransactionViewModel?> CreateTransactionAsync(TransactionFormModel transaction); // Método adicional para aceitar o modelo de formulário
        Task<TransactionViewModel?> UpdateTransactionAsync(string id, TransactionUpdateModel transaction); // Adicionado Async, alterado retorno para nullable
        Task<TransactionViewModel?> UpdateTransactionAsync(string id, TransactionFormModel transaction); // Método adicional para aceitar o modelo de formulário
        Task<bool> DeleteTransactionAsync(string id); // Mantido
        Task<List<TransactionViewModel>> GetFilteredTransactionsAsync(TransactionFilterModel filter); // Renomeado GetTransactionsAsync para GetFilteredTransactionsAsync
    }
}