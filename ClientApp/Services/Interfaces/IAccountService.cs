using FinanceManager.ClientApp.Models;

namespace FinanceManager.ClientApp.Services.Interfaces
{    public interface IAccountService
    {
        Task<IEnumerable<AccountViewModel>> GetAccountsAsync();
        Task<AccountViewModel> GetAccountByIdAsync(string id);
        Task<AccountViewModel> CreateAccountAsync(AccountFormModel model);
        Task<AccountViewModel> UpdateAccountAsync(AccountFormModel model);
        Task<bool> DeleteAccountAsync(string id);
        Task<IEnumerable<TransactionViewModel>> GetAccountTransactionsAsync(string accountId);
        Task<decimal> GetTotalBalanceAsync();
    }
}