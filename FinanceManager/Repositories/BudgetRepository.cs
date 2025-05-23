using FinanceManager.Data;
using FinanceManager.Models;
using FinanceManager.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Repositories
{
    /// <summary>
    /// Implementação do repositório de orçamentos
    /// </summary>
    public class BudgetRepository : Repository<Budget>, IBudgetRepository
    {
        public BudgetRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Budget>> GetByUserIdAsync(int userId)
        {
            return await _context.Budgets
                .Include(b => b.Category)
                .Where(b => b.UserId == userId)
                .OrderBy(b => b.Year)
                .ThenBy(b => b.Month)
                .ThenBy(b => b.Category.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Budget>> GetActiveBudgetsAsync(int userId)
        {
            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;

            return await _context.Budgets
                .Include(b => b.Category)
                .Where(b => b.UserId == userId && 
                      ((b.Year == currentYear && b.Month >= currentMonth) || b.Year > currentYear))
                .OrderBy(b => b.Year)
                .ThenBy(b => b.Month)
                .ThenBy(b => b.Category.Name)
                .ToListAsync();
        }

        public async Task<Budget?> GetByCategoryIdAndMonthAsync(int categoryId, int month, int year)
        {
            return await _context.Budgets
                .Include(b => b.Category)
                .FirstOrDefaultAsync(b => b.CategoryId == categoryId && b.Month == month && b.Year == year);
        }

        public async Task<IEnumerable<Budget>> GetByMonthAsync(int userId, int month, int year)
        {
            return await _context.Budgets
                .Include(b => b.Category)
                .Where(b => b.UserId == userId && b.Month == month && b.Year == year)
                .OrderBy(b => b.Category.Name)
                .ToListAsync();
        }
    }
}
