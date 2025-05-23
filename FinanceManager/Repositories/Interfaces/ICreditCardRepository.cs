using FinanceManager.Models;

namespace FinanceManager.Repositories.Interfaces
{
    /// <summary>
    /// Interface de repositório para a entidade CreditCard
    /// </summary>
    public interface ICreditCardRepository : IRepository<CreditCard>
    {
        Task<IEnumerable<CreditCard>> GetByUserIdAsync(int userId);
        Task<CreditCard?> GetByNumberAsync(string cardNumber);
    }
}
