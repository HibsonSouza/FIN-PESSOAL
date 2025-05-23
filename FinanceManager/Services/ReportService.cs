using FinanceManager.Models;
using FinanceManager.Models.Enums;
using FinanceManager.Repositories.Interfaces;

namespace FinanceManager.Services.Interfaces
{
    /// <summary>
    /// Interface para o serviço de relatórios
    /// </summary>
    public interface IReportService
    {
        Task<Dictionary<string, decimal>> GetMonthlyIncomeExpenseAsync(int userId, int year);
        Task<Dictionary<string, decimal>> GetCategoryBreakdownAsync(int userId, TransactionType type, DateTime startDate, DateTime endDate);
        Task<Dictionary<string, decimal>> GetAccountBalancesAsync(int userId);
        Task<Dictionary<DateTime, decimal>> GetDailyBalanceProgressAsync(int userId, DateTime startDate, DateTime endDate);
        Task<Dictionary<string, decimal>> GetBudgetVsActualAsync(int userId, int month, int year);
    }
}

namespace FinanceManager.Services
{
    /// <summary>
    /// Implementação do serviço de relatórios
    /// </summary>
    public class ReportService : IReportService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IBudgetRepository _budgetRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ReportService(
            ITransactionRepository transactionRepository,
            IAccountRepository accountRepository,
            IBudgetRepository budgetRepository,
            ICategoryRepository categoryRepository)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
            _budgetRepository = budgetRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<Dictionary<string, decimal>> GetMonthlyIncomeExpenseAsync(int userId, int year)
        {
            var result = new Dictionary<string, decimal>();
            var transactions = await _transactionRepository.GetByUserIdAsync(userId);

            // Filtrar por ano
            transactions = transactions.Where(t => t.Date.Year == year);

            // Agrupar por mês e tipo
            var groupedByMonth = transactions
                .GroupBy(t => new { Month = t.Date.Month, t.Type })
                .Select(g => new 
                {
                    Month = g.Key.Month,
                    Type = g.Key.Type,
                    Amount = g.Sum(t => t.Amount)
                })
                .OrderBy(x => x.Month);

            // Adicionar cada mês com receitas e despesas
            for (int month = 1; month <= 12; month++)
            {
                var monthName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
                var income = groupedByMonth.FirstOrDefault(x => x.Month == month && x.Type == TransactionType.Income)?.Amount ?? 0;
                var expense = groupedByMonth.FirstOrDefault(x => x.Month == month && x.Type == TransactionType.Expense)?.Amount ?? 0;

                result[$"{monthName}_income"] = income;
                result[$"{monthName}_expense"] = expense;
                result[$"{monthName}_balance"] = income - expense;
            }

            return result;
        }

        public async Task<Dictionary<string, decimal>> GetCategoryBreakdownAsync(int userId, TransactionType type, DateTime startDate, DateTime endDate)
        {
            var result = new Dictionary<string, decimal>();
            var transactions = await _transactionRepository.GetByDateRangeAsync(userId, startDate, endDate);

            // Filtrar por tipo
            transactions = transactions.Where(t => t.Type == type);

            // Agrupar por categoria
            var groupedByCategory = transactions
                .GroupBy(t => t.Category.Name)
                .Select(g => new
                {
                    CategoryName = g.Key,
                    Amount = g.Sum(t => t.Amount)
                })
                .OrderByDescending(x => x.Amount);

            foreach (var item in groupedByCategory)
            {
                result[item.CategoryName] = item.Amount;
            }

            return result;
        }

        public async Task<Dictionary<string, decimal>> GetAccountBalancesAsync(int userId)
        {
            var result = new Dictionary<string, decimal>();
            var accounts = await _accountRepository.GetByUserIdAsync(userId);

            foreach (var account in accounts)
            {
                result[account.Name] = account.Balance;
            }

            return result;
        }

        public async Task<Dictionary<DateTime, decimal>> GetDailyBalanceProgressAsync(int userId, DateTime startDate, DateTime endDate)
        {
            var result = new Dictionary<DateTime, decimal>();
            var transactions = await _transactionRepository.GetByDateRangeAsync(userId, startDate, endDate);
            var accounts = await _accountRepository.GetByUserIdAsync(userId);

            // Calcular o saldo inicial (saldo atual menos todas as transações no período)
            var initialBalance = accounts.Sum(a => a.Balance);
            foreach (var transaction in transactions)
            {
                if (transaction.Type == TransactionType.Income)
                {
                    initialBalance -= transaction.Amount;
                }
                else if (transaction.Type == TransactionType.Expense)
                {
                    initialBalance += transaction.Amount;
                }
            }

            // Incluir cada dia no período
            var currentBalance = initialBalance;
            for (var day = startDate; day <= endDate; day = day.AddDays(1))
            {
                var dayTransactions = transactions.Where(t => t.Date.Date == day.Date);
                
                foreach (var transaction in dayTransactions)
                {
                    if (transaction.Type == TransactionType.Income)
                    {
                        currentBalance += transaction.Amount;
                    }
                    else if (transaction.Type == TransactionType.Expense)
                    {
                        currentBalance -= transaction.Amount;
                    }
                }
                
                result[day] = currentBalance;
            }

            return result;
        }

        public async Task<Dictionary<string, decimal>> GetBudgetVsActualAsync(int userId, int month, int year)
        {
            var result = new Dictionary<string, decimal>();
            var budgets = await _budgetRepository.GetByMonthAsync(userId, month, year);
            
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            
            foreach (var budget in budgets)
            {
                var categoryTransactions = await _transactionRepository.GetByCategoryIdAsync(budget.CategoryId);
                var actualSpending = categoryTransactions
                    .Where(t => t.Type == TransactionType.Expense && t.Date >= startDate && t.Date <= endDate)
                    .Sum(t => t.Amount);
                
                result[$"{budget.Category.Name}_budget"] = budget.Amount;
                result[$"{budget.Category.Name}_actual"] = actualSpending;
                result[$"{budget.Category.Name}_diff"] = budget.Amount - actualSpending;
                result[$"{budget.Category.Name}_percent"] = budget.Amount > 0 
                    ? Math.Round((actualSpending / budget.Amount) * 100, 2) 
                    : 0;
            }
            
            return result;
        }
    }
}
