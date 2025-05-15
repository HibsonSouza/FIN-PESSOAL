using System.Collections.Generic;
using System.Threading.Tasks;
using FinanceManager.ClientApp.Models;

namespace FinanceManager.ClientApp.Services.Interfaces
{
    public interface IAccountService
    {
        Task<List<AccountViewModel>> GetAccountsAsync();
        Task<AccountViewModel> GetAccountByIdAsync(int id);
        Task<AccountViewModel> CreateAccountAsync(AccountCreateModel account);
        Task<AccountViewModel> UpdateAccountAsync(int id, AccountUpdateModel account);
        Task<bool> DeleteAccountAsync(int id);
        Task<decimal> GetTotalBalanceAsync();
        Task<List<TransactionViewModel>> GetAccountTransactionsAsync(int accountId, int count = 10);
    }
}