using FinanceManager.Data;
using FinanceManager.Models;
using FinanceManager.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Repositories
{
    /// <summary>
    /// Implementação do repositório de cartões de crédito
    /// </summary>
    public class CreditCardRepository : Repository<CreditCard>, ICreditCardRepository
    {
        public CreditCardRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<CreditCard>> GetByUserIdAsync(int userId)
        {
            return await _context.CreditCards
                .Where(c => c.UserId == userId)
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<CreditCard?> GetByNumberAsync(string cardNumber)
        {
            return await _context.CreditCards
                .FirstOrDefaultAsync(c => c.CardNumber == cardNumber);
        }
    }
}
