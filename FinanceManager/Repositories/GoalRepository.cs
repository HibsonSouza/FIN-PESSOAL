using FinanceManager.Data;
using FinanceManager.Models;
using FinanceManager.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Repositories
{
    /// <summary>
    /// Implementação do repositório de metas financeiras
    /// </summary>
    public class GoalRepository : Repository<Goal>, IGoalRepository
    {
        public GoalRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Goal>> GetByUserIdAsync(int userId)
        {
            return await _context.Goals
                .Where(g => g.UserId == userId)
                .OrderBy(g => g.TargetDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Goal>> GetActiveGoalsAsync(int userId)
        {
            return await _context.Goals
                .Where(g => g.UserId == userId && g.CurrentAmount < g.TargetAmount && g.TargetDate >= DateTime.Now)
                .OrderBy(g => g.TargetDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Goal>> GetCompletedGoalsAsync(int userId)
        {
            return await _context.Goals
                .Where(g => g.UserId == userId && g.CurrentAmount >= g.TargetAmount)
                .OrderByDescending(g => g.UpdatedAt)
                .ToListAsync();
        }
    }
}
