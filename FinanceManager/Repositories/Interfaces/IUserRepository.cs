using FinanceManager.Models;

namespace FinanceManager.Repositories.Interfaces
{
    /// <summary>
    /// Interface de repositório para a entidade User
    /// </summary>
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<bool> EmailExistsAsync(string email);
    }
}
