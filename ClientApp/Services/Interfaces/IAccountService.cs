using FinanceManager.ClientApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceManager.ClientApp.Services
{
    public interface IAccountService
    {
        Task<List<AccountViewModel>> GetAccountsAsync();
        Task<AccountViewModel> GetAccountByIdAsync(string id);
        Task<AccountViewModel> CreateAccountAsync(AccountCreateModel account);
        Task<AccountViewModel> UpdateAccountAsync(string id, AccountUpdateModel account);
        Task<bool> DeleteAccountAsync(string id);
        Task<List<AccountBalanceHistoryViewModel>> GetAccountBalanceHistoryAsync(string id, DateTime? startDate = null, DateTime? endDate = null);
    }
}