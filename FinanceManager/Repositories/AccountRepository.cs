using FinanceManager.Data;
using FinanceManager.Models;
using FinanceManager.Models.Enums;
using FinanceManager.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Repositories
{
    /// <summary>
    /// Implementação do repositório de contas
    /// </summary>
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Account>> GetByUserIdAsync(int userId)
        {
            return await _context.Accounts
                .Where(a => a.UserId == userId)
                .OrderBy(a => a.Name)
                .ToListAsync();
        }

        public async Task<decimal> GetTotalBalanceAsync(int userId)
        {
            return await _context.Accounts
                .Where(a => a.UserId == userId)
                .SumAsync(a => a.Balance);
        }

        public async Task<IEnumerable<Account>> GetByTypeAsync(int userId, AccountType type)
        {
            return await _context.Accounts
                .Where(a => a.UserId == userId && a.Type == type)
                .OrderBy(a => a.Name)
                .ToListAsync();
        }
    }
}
