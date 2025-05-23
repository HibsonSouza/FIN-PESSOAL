using FinanceManager.Models;
using FinanceManager.Models.Enums;
using FinanceManager.Repositories.Interfaces;

namespace FinanceManager.Services.Interfaces
{
    /// <summary>
    /// Interface para o serviço de transações
    /// </summary>
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetAllTransactionsAsync(int userId);
        Task<IEnumerable<Transaction>> GetTransactionsByAccountAsync(int accountId);
        Task<IEnumerable<Transaction>> GetTransactionsByCategoryAsync(int categoryId);
        Task<IEnumerable<Transaction>> GetTransactionsByTypeAsync(int userId, TransactionType type);
        Task<IEnumerable<Transaction>> GetTransactionsByDateRangeAsync(int userId, DateTime startDate, DateTime endDate);
        Task<IEnumerable<Transaction>> GetFilteredTransactionsAsync(
            int userId, 
            DateTime? startDate, 
            DateTime? endDate, 
            int? accountId, 
            int? categoryId, 
            TransactionType? type, 
            string? searchTerm);
        Task<Transaction?> GetTransactionByIdAsync(int id);
        Task<Transaction> CreateTransactionAsync(Transaction transaction);
        Task<Transaction> UpdateTransactionAsync(Transaction transaction);
        Task DeleteTransactionAsync(int id);
        Task<decimal> GetTotalByTypeAndPeriodAsync(int userId, TransactionType type, DateTime startDate, DateTime endDate);
        Task<IEnumerable<Transaction>> GetRecentTransactionsAsync(int userId, int count = 5);
    }
}

namespace FinanceManager.Services
{
    /// <summary>
    /// Implementação do serviço de transações
    /// </summary>
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;

        public TransactionService(
            ITransactionRepository transactionRepository,
            IAccountRepository accountRepository)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync(int userId)
        {
            return await _transactionRepository.GetByUserIdAsync(userId);
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByAccountAsync(int accountId)
        {
            return await _transactionRepository.GetByAccountIdAsync(accountId);
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByCategoryAsync(int categoryId)
        {
            return await _transactionRepository.GetByCategoryIdAsync(categoryId);
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByTypeAsync(int userId, TransactionType type)
        {
            return await _transactionRepository.GetByTypeAsync(userId, type);
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByDateRangeAsync(int userId, DateTime startDate, DateTime endDate)
        {
            return await _transactionRepository.GetByDateRangeAsync(userId, startDate, endDate);
        }

        public async Task<IEnumerable<Transaction>> GetFilteredTransactionsAsync(
            int userId, 
            DateTime? startDate, 
            DateTime? endDate, 
            int? accountId, 
            int? categoryId, 
            TransactionType? type, 
            string? searchTerm)
        {
            return await _transactionRepository.GetFilteredTransactionsAsync(
                userId, startDate, endDate, accountId, categoryId, type, searchTerm);
        }

        public async Task<Transaction?> GetTransactionByIdAsync(int id)
        {
            return await _transactionRepository.GetByIdAsync(id);
        }

        public async Task<Transaction> CreateTransactionAsync(Transaction transaction)
        {
            // Atualizar o saldo da conta
            var account = await _accountRepository.GetByIdAsync(transaction.AccountId);
            if (account != null)
            {
                if (transaction.Type == TransactionType.Income)
                {
                    account.Balance += transaction.Amount;
                }
                else if (transaction.Type == TransactionType.Expense)
                {
                    account.Balance -= transaction.Amount;
                }
                
                await _accountRepository.UpdateAsync(account);
            }
            
            return await _transactionRepository.CreateAsync(transaction);
        }

        public async Task<Transaction> UpdateTransactionAsync(Transaction transaction)
        {
            // Obter a transação original para restaurar o saldo antes de fazer a atualização
            var originalTransaction = await _transactionRepository.GetByIdAsync(transaction.Id);
            if (originalTransaction != null)
            {
                var account = await _accountRepository.GetByIdAsync(originalTransaction.AccountId);
                if (account != null)
                {
                    // Reverter o efeito da transação original
                    if (originalTransaction.Type == TransactionType.Income)
                    {
                        account.Balance -= originalTransaction.Amount;
                    }
                    else if (originalTransaction.Type == TransactionType.Expense)
                    {
                        account.Balance += originalTransaction.Amount;
                    }
                    
                    // Se a conta mudou, atualizar a conta original e obter a nova conta
                    if (transaction.AccountId != originalTransaction.AccountId)
                    {
                        await _accountRepository.UpdateAsync(account);
                        account = await _accountRepository.GetByIdAsync(transaction.AccountId);
                    }
                    
                    if (account != null)
                    {
                        // Aplicar o efeito da nova transação
                        if (transaction.Type == TransactionType.Income)
                        {
                            account.Balance += transaction.Amount;
                        }
                        else if (transaction.Type == TransactionType.Expense)
                        {
                            account.Balance -= transaction.Amount;
                        }
                        
                        await _accountRepository.UpdateAsync(account);
                    }
                }
            }
            
            return await _transactionRepository.UpdateAsync(transaction);
        }

        public async Task DeleteTransactionAsync(int id)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);
            if (transaction != null)
            {
                // Atualizar o saldo da conta
                var account = await _accountRepository.GetByIdAsync(transaction.AccountId);
                if (account != null)
                {
                    if (transaction.Type == TransactionType.Income)
                    {
                        account.Balance -= transaction.Amount;
                    }
                    else if (transaction.Type == TransactionType.Expense)
                    {
                        account.Balance += transaction.Amount;
                    }
                    
                    await _accountRepository.UpdateAsync(account);
                }
                
                await _transactionRepository.DeleteAsync(transaction);
            }
        }

        public async Task<decimal> GetTotalByTypeAndPeriodAsync(int userId, TransactionType type, DateTime startDate, DateTime endDate)
        {
            return await _transactionRepository.GetTotalByTypeAndPeriodAsync(userId, type, startDate, endDate);
        }

        public async Task<IEnumerable<Transaction>> GetRecentTransactionsAsync(int userId, int count = 5)
        {
            return await _transactionRepository.GetRecentTransactionsAsync(userId, count);
        }
    }
}
