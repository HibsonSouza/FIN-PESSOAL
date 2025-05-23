using FinanceManager.Models;
using FinanceManager.Repositories.Interfaces;

namespace FinanceManager.Services.Interfaces
{
    /// <summary>
    /// Interface para o serviço de orçamentos
    /// </summary>
    public interface IBudgetService
    {
        Task<IEnumerable<Budget>> GetAllBudgetsAsync(int userId);
        Task<IEnumerable<Budget>> GetActiveBudgetsAsync(int userId);
        Task<IEnumerable<Budget>> GetBudgetsByMonthAsync(int userId, int month, int year);
        Task<Budget?> GetBudgetByIdAsync(int id);
        Task<Budget?> GetBudgetByCategoryAndMonthAsync(int categoryId, int month, int year);
        Task<Budget> CreateBudgetAsync(Budget budget);
        Task<Budget> UpdateBudgetAsync(Budget budget);
        Task DeleteBudgetAsync(int id);
    }
}

namespace FinanceManager.Services
{
    /// <summary>
    /// Implementação do serviço de orçamentos
    /// </summary>
    public class BudgetService : IBudgetService
    {
        private readonly IBudgetRepository _budgetRepository;

        public BudgetService(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        public async Task<IEnumerable<Budget>> GetAllBudgetsAsync(int userId)
        {
            return await _budgetRepository.GetByUserIdAsync(userId);
        }

        public async Task<IEnumerable<Budget>> GetActiveBudgetsAsync(int userId)
        {
            return await _budgetRepository.GetActiveBudgetsAsync(userId);
        }

        public async Task<IEnumerable<Budget>> GetBudgetsByMonthAsync(int userId, int month, int year)
        {
            return await _budgetRepository.GetByMonthAsync(userId, month, year);
        }

        public async Task<Budget?> GetBudgetByIdAsync(int id)
        {
            return await _budgetRepository.GetByIdAsync(id);
        }

        public async Task<Budget?> GetBudgetByCategoryAndMonthAsync(int categoryId, int month, int year)
        {
            return await _budgetRepository.GetByCategoryIdAndMonthAsync(categoryId, month, year);
        }

        public async Task<Budget> CreateBudgetAsync(Budget budget)
        {
            return await _budgetRepository.CreateAsync(budget);
        }

        public async Task<Budget> UpdateBudgetAsync(Budget budget)
        {
            return await _budgetRepository.UpdateAsync(budget);
        }

        public async Task DeleteBudgetAsync(int id)
        {
            var budget = await _budgetRepository.GetByIdAsync(id);
            if (budget != null)
            {
                await _budgetRepository.DeleteAsync(budget);
            }
        }
    }
}
