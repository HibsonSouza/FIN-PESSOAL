using FinanceManager.ClientApp.Models;

namespace FinanceManager.ClientApp.Services.Interfaces
{
    public interface IAccountService
    {
        Task<List<AccountViewModel>> GetAccounts();
        Task<AccountViewModel> GetAccountById(string id);
        Task<bool> CreateAccount(AccountCreateModel account);
        Task<bool> UpdateAccount(string id, AccountUpdateModel account);
        Task<bool> DeleteAccount(string id);
        Task<decimal> GetTotalBalance();
    }
}