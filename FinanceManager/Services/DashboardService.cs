using FinanceManager.Models;
using FinanceManager.Models.Enums;
using FinanceManager.Repositories.Interfaces;

namespace FinanceManager.Services.Interfaces
{
    /// <summary>
    /// Interface para o serviço de dashboard
    /// </summary>
    public interface IDashboardService
    {
        Task<Dictionary<string, object>> GetDashboardDataAsync(int userId, DateRange dateRange);
        Task<decimal> GetTotalBalanceAsync(int userId);
        Task<decimal> GetMonthlyIncomeAsync(int userId, DateTime date);
        Task<decimal> GetMonthlyExpensesAsync(int userId, DateTime date);
        Task<List<CategorySummary>> GetExpensesByCategoryAsync(int userId, DateRange dateRange);
        Task<List<CashFlowItem>> GetCashFlowAsync(int userId, DateRange dateRange);
        Task<List<BudgetProgress>> GetBudgetProgressAsync(int userId);
        Task<List<SavingsGoalProgress>> GetSavingsGoalsProgressAsync(int userId);
    }
}

namespace FinanceManager.Models
{
    public class DateRange
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class CategorySummary
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public double Percentage { get; set; }
        public string Color { get; set; }
        public TransactionType Type { get; set; }
    }

    public class CashFlowItem
    {
        public DateTime Date { get; set; }
        public decimal Income { get; set; }
        public decimal Expenses { get; set; }
        public decimal Balance { get; set; }
        public string Period { get; set; }
    }

    public class BudgetProgress
    {
        public int BudgetId { get; set; }
        public string Name { get; set; }
        public decimal BudgetAmount { get; set; }
        public decimal CurrentSpent { get; set; }
        public decimal RemainingAmount => BudgetAmount - CurrentSpent;
        public double PercentageUsed => Math.Min(100, Math.Round((double)(CurrentSpent / BudgetAmount * 100), 1));
        public string CategoryName { get; set; }
        public string CategoryIcon { get; set; }
        public string CategoryColor { get; set; }
        public bool IsOverBudget => CurrentSpent > BudgetAmount;
        public double DaysRemainingPercentage { get; set; }
    }

    public class SavingsGoalProgress
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal TargetAmount { get; set; }
        public decimal CurrentAmount { get; set; }
        public double PercentComplete { get; set; }
        public DateTime? TargetDate { get; set; }
        public string Color { get; set; }
    }
}

namespace FinanceManager.Services
{
    /// <summary>
    /// Implementação do serviço de dashboard
    /// </summary>
    public class DashboardService : IDashboardService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBudgetRepository _budgetRepository;
        private readonly IGoalRepository _goalRepository;

        public DashboardService(
            IAccountRepository accountRepository,
            ITransactionRepository transactionRepository,
            ICategoryRepository categoryRepository,
            IBudgetRepository budgetRepository,
            IGoalRepository goalRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
            _categoryRepository = categoryRepository;
            _budgetRepository = budgetRepository;
            _goalRepository = goalRepository;        }

        public async Task<Dictionary<string, object>> GetDashboardDataAsync(int userId, DateRange dateRange)
        {
            var result = new Dictionary<string, object>();

            // Dados básicos
            result["TotalBalance"] = await _accountRepository.GetTotalBalanceAsync(userId);
            
            // Transações recentes
            result["RecentTransactions"] = await _transactionRepository.GetRecentTransactionsAsync(userId, 5);

            // Dados de receitas e despesas do mês atual
            var now = DateTime.Now;
            var startOfMonth = new DateTime(now.Year, now.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);
            
            var totalIncome = await _transactionRepository.GetTotalByTypeAndPeriodAsync(
                userId, TransactionType.Income, startOfMonth, endOfMonth);
            var totalExpense = await _transactionRepository.GetTotalByTypeAndPeriodAsync(
                userId, TransactionType.Expense, startOfMonth, endOfMonth);
            
            result["MonthlyIncome"] = totalIncome;
            result["MonthlyExpense"] = totalExpense;
            result["MonthlyBalance"] = totalIncome - totalExpense;

            // Dados do mês anterior para comparação
            var startOfLastMonth = startOfMonth.AddMonths(-1);
            var endOfLastMonth = startOfMonth.AddDays(-1);
            
            var lastMonthIncome = await _transactionRepository.GetTotalByTypeAndPeriodAsync(
                userId, TransactionType.Income, startOfLastMonth, endOfLastMonth);
            var lastMonthExpense = await _transactionRepository.GetTotalByTypeAndPeriodAsync(
                userId, TransactionType.Expense, startOfLastMonth, endOfLastMonth);
            
            result["LastMonthIncome"] = lastMonthIncome;
            result["LastMonthExpense"] = lastMonthExpense;
            result["LastMonthBalance"] = lastMonthIncome - lastMonthExpense;

            // Calcular variações percentuais
            result["IncomeVariation"] = lastMonthIncome > 0 
                ? Math.Round(((totalIncome - lastMonthIncome) / lastMonthIncome) * 100, 2) 
                : 100;
            result["ExpenseVariation"] = lastMonthExpense > 0 
                ? Math.Round(((totalExpense - lastMonthExpense) / lastMonthExpense) * 100, 2) 
                : 100;

            // Orçamentos ativos
            var activeBudgets = await _budgetRepository.GetActiveBudgetsAsync(userId);
            // Filtrar apenas para o mês atual
            var currentMonthBudgets = activeBudgets.Where(b => 
                (b.StartDate <= endOfMonth && (b.EndDate == null || b.EndDate >= startOfMonth))).ToList();
                
            foreach (var budget in currentMonthBudgets)
            {
                if (budget.CategoryId.HasValue)
                {
                    var categoryTransactions = await _transactionRepository.GetByCategoryIdAsync(budget.CategoryId.Value);
                    var actualSpending = categoryTransactions
                        .Where(t => t.Type == TransactionType.Expense && t.Date >= startOfMonth && t.Date <= endOfMonth)
                        .Sum(t => t.Amount);
                    
                    // Não temos CurrentAmount no modelo, então vamos adicionar isso ao dicionário
                    result[$"Budget_{budget.Id}_CurrentAmount"] = actualSpending;
                }
            }
            
            result["ActiveBudgets"] = currentMonthBudgets;

            // Metas ativas
            result["ActiveGoals"] = await _goalRepository.GetActiveGoalsAsync(userId);

            // Distribuição de despesas por categoria
            var transactions = await _transactionRepository.GetByDateRangeAsync(userId, startOfMonth, endOfMonth);
            var expensesByCategory = transactions
                .Where(t => t.Type == TransactionType.Expense)
                .GroupBy(t => new { t.CategoryId, CategoryName = t.Category?.Name ?? "Sem categoria" })
                .Select(g => new
                {
                    CategoryId = g.Key.CategoryId,
                    CategoryName = g.Key.CategoryName,
                    TotalAmount = g.Sum(t => t.Amount)
                })
                .OrderByDescending(x => x.TotalAmount)
                .ToList();
                
            result["ExpensesByCategory"] = expensesByCategory;

            return result;
        }

        public async Task<decimal> GetTotalBalanceAsync(int userId)
        {
            return await _accountRepository.GetTotalBalanceAsync(userId);
        }

        public async Task<decimal> GetMonthlyIncomeAsync(int userId, DateTime date)
        {
            var startOfMonth = new DateTime(date.Year, date.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);
            
            return await _transactionRepository.GetTotalByTypeAndPeriodAsync(
                userId, TransactionType.Income, startOfMonth, endOfMonth);        }

        public async Task<decimal> GetMonthlyExpensesAsync(int userId, DateTime date)
        {
            var startOfMonth = new DateTime(date.Year, date.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);
            
            return await _transactionRepository.GetTotalByTypeAndPeriodAsync(
                userId, TransactionType.Expense, startOfMonth, endOfMonth);
        }

        public async Task<List<CategorySummary>> GetExpensesByCategoryAsync(int userId, DateRange dateRange)
        {
            var transactions = await _transactionRepository.GetByDateRangeAsync(userId, dateRange.StartDate, dateRange.EndDate);
            var expenseTransactions = transactions.Where(t => t.Type == TransactionType.Expense).ToList();
            
            if (!expenseTransactions.Any())
                return new List<CategorySummary>();

            // Calcular o total de despesas
            var totalExpenses = expenseTransactions.Sum(t => t.Amount);
            
            // Agrupar despesas por categoria e calcular totais e percentagens
            var categories = (await _categoryRepository.GetByUserIdAsync(userId)).ToDictionary(c => c.Id);
            
            var result = expenseTransactions
                .GroupBy(t => t.CategoryId)
                .Select(g => 
                {
                    var category = categories.ContainsKey(g.Key) ? categories[g.Key] : null;
                    var amount = g.Sum(t => t.Amount);
                    var percentage = totalExpenses > 0 ? (double)amount / (double)totalExpenses * 100 : 0;
                    
                    return new CategorySummary
                    {
                        Id = g.Key.ToString(),
                        Name = category?.Name ?? "Sem categoria",
                        Amount = amount,
                        Percentage = Math.Round(percentage, 2),
                        Color = category?.Color ?? "#808080", // Cinza como cor padrão
                        Type = TransactionType.Expense
                    };
                })
                .OrderByDescending(c => c.Amount)
                .ToList();
            
            return result;
        }

        public async Task<List<CashFlowItem>> GetCashFlowAsync(int userId, DateRange dateRange)
        {
            // Determinar a granularidade do gráfico com base no intervalo de datas
            // Se o período for menor que 60 dias, mostrar dados diários
            // Caso contrário, agrupar por semana ou mês
            TimeSpan interval = dateRange.EndDate - dateRange.StartDate;
            bool showDaily = interval.TotalDays <= 60;
            bool showWeekly = interval.TotalDays > 60 && interval.TotalDays <= 180;
            // Se não for diário nem semanal, será mensal
            
            var transactions = await _transactionRepository.GetByDateRangeAsync(userId, dateRange.StartDate, dateRange.EndDate);
            var result = new List<CashFlowItem>();
            
            if (showDaily)
            {
                // Agrupar por dia
                var dailyGroups = transactions
                    .GroupBy(t => t.Date.Date)
                    .OrderBy(g => g.Key);
                
                foreach (var group in dailyGroups)
                {
                    var income = group.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount);
                    var expenses = group.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount);
                    
                    result.Add(new CashFlowItem
                    {
                        Date = group.Key,
                        Income = income,
                        Expenses = expenses,
                        Balance = income - expenses,
                        Period = group.Key.ToString("dd/MM")
                    });
                }
            }
            else if (showWeekly)
            {
                // Agrupar por semana
                var groupedByWeek = transactions
                    .GroupBy(t => new { 
                        Year = t.Date.Year, 
                        Week = GetIso8601WeekOfYear(t.Date)
                    })
                    .OrderBy(g => g.Key.Year)
                    .ThenBy(g => g.Key.Week);
                
                foreach (var group in groupedByWeek)
                {
                    // Pegar a data do primeiro dia da semana para representar a semana
                    var firstDay = group.Min(t => t.Date);
                    var income = group.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount);
                    var expenses = group.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount);
                    
                    result.Add(new CashFlowItem
                    {
                        Date = firstDay,
                        Income = income,
                        Expenses = expenses,
                        Balance = income - expenses,
                        Period = $"Sem {group.Key.Week}/{group.Key.Year}"
                    });
                }
            }
            else
            {
                // Agrupar por mês
                var groupedByMonth = transactions
                    .GroupBy(t => new { t.Date.Year, t.Date.Month })
                    .OrderBy(g => g.Key.Year)
                    .ThenBy(g => g.Key.Month);
                
                foreach (var group in groupedByMonth)
                {
                    var monthDate = new DateTime(group.Key.Year, group.Key.Month, 1);
                    var income = group.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount);
                    var expenses = group.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount);
                    
                    result.Add(new CashFlowItem
                    {
                        Date = monthDate,
                        Income = income,
                        Expenses = expenses,
                        Balance = income - expenses,
                        Period = monthDate.ToString("MMM/yyyy")
                    });
                }
            }
            
            // Se não houver transações no período, retornar pelo menos um item com valores zerados
            if (!result.Any())
            {
                result.Add(new CashFlowItem
                {
                    Date = dateRange.StartDate,
                    Income = 0,
                    Expenses = 0,
                    Balance = 0,
                    Period = dateRange.StartDate.ToString("dd/MM/yyyy")
                });
            }
              return result;
        }
        
        /// <summary>
        /// Retorna o número da semana no ano, de acordo com ISO 8601
        /// </summary>
        private int GetIso8601WeekOfYear(DateTime date)
        {
            var cal = System.Globalization.CultureInfo.InvariantCulture.Calendar;
            return cal.GetWeekOfYear(date, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public async Task<List<BudgetProgress>> GetBudgetProgressAsync(int userId)
        {
            // Obter o mês atual
            var now = DateTime.Now;
            var startOfMonth = new DateTime(now.Year, now.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);
            
            // Obter todos os orçamentos ativos do usuário
            var allBudgets = await _budgetRepository.GetActiveBudgetsAsync(userId);
            
            // Filtrar apenas os orçamentos relevantes para o mês atual
            var budgets = allBudgets.Where(b => 
                (b.StartDate <= endOfMonth && (b.EndDate == null || b.EndDate >= startOfMonth))).ToList();
                
            if (!budgets.Any())
                return new List<BudgetProgress>();
                
            var result = new List<BudgetProgress>();
            
            // Calcular o progresso para cada orçamento
            foreach (var budget in budgets)
            {
                // Obter transações da categoria deste orçamento
                var categoryTransactions = budget.CategoryId.HasValue 
                    ? await _transactionRepository.GetByCategoryIdAsync(budget.CategoryId.Value)
                    : new List<Transaction>();
                    
                // Filtrar transações pelo período e tipo (despesa)
                var relevantTransactions = categoryTransactions
                    .Where(t => t.Type == TransactionType.Expense && 
                                t.Date >= startOfMonth && 
                                t.Date <= endOfMonth)
                    .ToList();
                
                // Calcular o valor gasto até agora
                var currentSpent = relevantTransactions.Sum(t => t.Amount);
                
                // Obter a categoria para informações adicionais
                var category = budget.CategoryId.HasValue 
                    ? await _categoryRepository.GetByIdAsync(budget.CategoryId.Value) 
                    : null;
                    
                // Calcular a porcentagem de dias do mês já decorridos
                double totalDaysInMonth = (endOfMonth - startOfMonth).TotalDays + 1;
                double daysElapsed = (now - startOfMonth).TotalDays + 1;
                double daysRemainingPercentage = Math.Min(100, Math.Round((daysElapsed / totalDaysInMonth) * 100, 1));
                
                result.Add(new BudgetProgress
                {
                    BudgetId = budget.Id,
                    Name = budget.Name,
                    BudgetAmount = budget.Amount,
                    CurrentSpent = currentSpent,
                    CategoryName = category?.Name ?? "Geral",
                    CategoryIcon = category?.Icon ?? "dollar-sign",
                    CategoryColor = category?.Color ?? "#808080",
                    DaysRemainingPercentage = daysRemainingPercentage
                });
            }
              // Ordenar por porcentagem de uso em ordem decrescente (orçamentos mais próximos do limite primeiro)
            return result.OrderByDescending(b => b.PercentageUsed).ToList();
        }

        public async Task<List<SavingsGoalProgress>> GetSavingsGoalsProgressAsync(int userId)
        {
            // Obter todas as metas de economia ativas do usuário
            var goals = await _goalRepository.GetActiveGoalsAsync(userId);
            if (!goals.Any())
                return new List<SavingsGoalProgress>();
                
            var result = new List<SavingsGoalProgress>();
            
            foreach (var goal in goals)
            {
                // Calcular porcentagem completa
                double percentComplete = goal.TargetAmount > 0 
                    ? Math.Min(100, Math.Round((double)(goal.CurrentAmount / goal.TargetAmount * 100), 1)) 
                    : 0;
                    
                result.Add(new SavingsGoalProgress
                {
                    Id = goal.Id.ToString(),
                    Name = goal.Name,
                    TargetAmount = goal.TargetAmount,
                    CurrentAmount = goal.CurrentAmount,
                    PercentComplete = percentComplete,
                    TargetDate = goal.TargetDate,
                    Color = goal.Color ?? "#4CAF50" // Verde como cor padrão
                });
            }
            
            // Ordenar por percentual de conclusão em ordem decrescente
            return result.OrderByDescending(g => g.PercentComplete).ToList();
        }
    }
}
