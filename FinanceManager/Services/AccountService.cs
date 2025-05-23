using FinanceManager.Models;
using FinanceManager.Repositories.Interfaces;

namespace FinanceManager.Services.Interfaces
{
    /// <summary>
    /// Interface para o serviço de contas financeiras
    /// </summary>
    public interface IAccountService
    {
        Task<IEnumerable<Account>> GetAllAccountsAsync(int userId);
        Task<Account?> GetAccountByIdAsync(int id);
        Task<Account> CreateAccountAsync(Account account);
        Task<Account> UpdateAccountAsync(Account account);
        Task DeleteAccountAsync(int id);
        Task<decimal> GetTotalBalanceAsync(int userId);
    }
}

namespace FinanceManager.Services
{
    /// <summary>
    /// Implementação do serviço de contas financeiras
    /// </summary>
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<IEnumerable<Account>> GetAllAccountsAsync(int userId)
        {
            return await _accountRepository.GetByUserIdAsync(userId);
        }

        public async Task<Account?> GetAccountByIdAsync(int id)
        {
            return await _accountRepository.GetByIdAsync(id);
        }

        public async Task<Account> CreateAccountAsync(Account account)
        {
            return await _accountRepository.CreateAsync(account);
        }

        public async Task<Account> UpdateAccountAsync(Account account)
        {
            return await _accountRepository.UpdateAsync(account);
        }

        public async Task DeleteAccountAsync(int id)
        {
            var account = await _accountRepository.GetByIdAsync(id);
            if (account != null)
            {
                await _accountRepository.DeleteAsync(account);
            }
        }

        public async Task<decimal> GetTotalBalanceAsync(int userId)
        {
            return await _accountRepository.GetTotalBalanceAsync(userId);
        }
    }
}
