using FinanceManager.ClientApp.Models;
using FinanceManager.ClientApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FinanceManager.ClientApp.Services
{
    public class ReportService : IReportService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "api/reports";

        public ReportService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IncomeExpenseReportResult> GetIncomeExpenseReportAsync(DateRange dateRange)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<IncomeExpenseReportResult>(
                    $"{_apiUrl}/income-expense?startDate={dateRange.StartDate:yyyy-MM-dd}&endDate={dateRange.EndDate:yyyy-MM-dd}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar relatório de receitas e despesas: {ex.Message}");
                return CreateMockIncomeExpenseReport(dateRange);
            }
        }

        public async Task<CategoryDistributionReportResult> GetCategoryDistributionReportAsync(DateRange dateRange, TransactionType type)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<CategoryDistributionReportResult>(
                    $"{_apiUrl}/category-distribution?startDate={dateRange.StartDate:yyyy-MM-dd}&endDate={dateRange.EndDate:yyyy-MM-dd}&type={type}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar relatório de distribuição por categoria: {ex.Message}");
                return CreateMockCategoryDistributionReport(dateRange, type);
            }
        }

        public async Task<CashflowReportResult> GetCashflowReportAsync(DateRange dateRange)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<CashflowReportResult>(
                    $"{_apiUrl}/cashflow?startDate={dateRange.StartDate:yyyy-MM-dd}&endDate={dateRange.EndDate:yyyy-MM-dd}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar relatório de fluxo de caixa: {ex.Message}");
                return CreateMockCashflowReport(dateRange);
            }
        }

        public async Task<NetWorthReportResult> GetNetWorthReportAsync(DateRange dateRange)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<NetWorthReportResult>(
                    $"{_apiUrl}/net-worth?startDate={dateRange.StartDate:yyyy-MM-dd}&endDate={dateRange.EndDate:yyyy-MM-dd}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar relatório de patrimônio líquido: {ex.Message}");
                return CreateMockNetWorthReport(dateRange);
            }
        }

        public async Task<AccountBalanceHistoryReportResult> GetAccountBalanceHistoryReportAsync(string accountId, DateRange dateRange)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<AccountBalanceHistoryReportResult>(
                    $"{_apiUrl}/account-balance-history/{accountId}?startDate={dateRange.StartDate:yyyy-MM-dd}&endDate={dateRange.EndDate:yyyy-MM-dd}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar histórico de saldo da conta: {ex.Message}");
                return CreateMockAccountBalanceHistoryReport(accountId, dateRange);
            }
        }

        public async Task<BudgetPerformanceReportResult> GetBudgetPerformanceReportAsync(DateRange dateRange)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<BudgetPerformanceReportResult>(
                    $"{_apiUrl}/budget-performance?startDate={dateRange.StartDate:yyyy-MM-dd}&endDate={dateRange.EndDate:yyyy-MM-dd}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar relatório de desempenho do orçamento: {ex.Message}");
                return CreateMockBudgetPerformanceReport(dateRange);
            }
        }

        public async Task<List<TransactionReportItem>> GetTransactionReportAsync(TransactionReportFilter filter)
        {
            try
            {
                // Construa a URL com todos os parâmetros do filtro
                var queryParameters = new List<string>
                {
                    $"startDate={filter.DateRange.StartDate:yyyy-MM-dd}",
                    $"endDate={filter.DateRange.EndDate:yyyy-MM-dd}",
                    $"page={filter.Page}",
                    $"pageSize={filter.PageSize}",
                    $"sortBy={filter.SortBy}",
                    $"sortAscending={filter.SortAscending}",
                    $"includePending={filter.IncludePending}"
                };

                if (filter.MinAmount.HasValue)
                    queryParameters.Add($"minAmount={filter.MinAmount.Value}");

                if (filter.MaxAmount.HasValue)
                    queryParameters.Add($"maxAmount={filter.MaxAmount.Value}");

                if (!string.IsNullOrEmpty(filter.SearchTerm))
                    queryParameters.Add($"searchTerm={Uri.EscapeDataString(filter.SearchTerm)}");

                if (filter.AccountIds.Any())
                    queryParameters.Add($"accountIds={string.Join(",", filter.AccountIds)}");

                if (filter.CategoryIds.Any())
                    queryParameters.Add($"categoryIds={string.Join(",", filter.CategoryIds)}");

                if (filter.TransactionTypes.Any())
                    queryParameters.Add($"types={string.Join(",", filter.TransactionTypes)}");

                var url = $"{_apiUrl}/transactions?{string.Join("&", queryParameters)}";
                return await _httpClient.GetFromJsonAsync<List<TransactionReportItem>>(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar relatório de transações: {ex.Message}");
                return CreateMockTransactionReport(filter);
            }
        }

        public async Task<ExpensesTrendReportResult> GetExpensesTrendReportAsync(DateRange dateRange, string categoryId = null)
        {
            try
            {
                var url = $"{_apiUrl}/expenses-trend?startDate={dateRange.StartDate:yyyy-MM-dd}&endDate={dateRange.EndDate:yyyy-MM-dd}";
                if (!string.IsNullOrEmpty(categoryId))
                    url += $"&categoryId={categoryId}";

                return await _httpClient.GetFromJsonAsync<ExpensesTrendReportResult>(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar relatório de tendência de despesas: {ex.Message}");
                return CreateMockExpensesTrendReport(dateRange);
            }
        }

        public async Task<IncomeTrendReportResult> GetIncomeTrendReportAsync(DateRange dateRange, string categoryId = null)
        {
            try
            {
                var url = $"{_apiUrl}/income-trend?startDate={dateRange.StartDate:yyyy-MM-dd}&endDate={dateRange.EndDate:yyyy-MM-dd}";
                if (!string.IsNullOrEmpty(categoryId))
                    url += $"&categoryId={categoryId}";

                return await _httpClient.GetFromJsonAsync<IncomeTrendReportResult>(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar relatório de tendência de receitas: {ex.Message}");
                return CreateMockIncomeTrendReport(dateRange);
            }
        }

        public async Task<SavingsRateReportResult> GetSavingsRateReportAsync(DateRange dateRange)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<SavingsRateReportResult>(
                    $"{_apiUrl}/savings-rate?startDate={dateRange.StartDate:yyyy-MM-dd}&endDate={dateRange.EndDate:yyyy-MM-dd}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar relatório de taxa de poupança: {ex.Message}");
                return CreateMockSavingsRateReport(dateRange);
            }
        }

        public async Task<ChartData> GetReportChartDataAsync(ReportType reportType, DateRange dateRange, string additionalFilter = null)
        {
            try
            {
                var url = $"{_apiUrl}/chart-data/{reportType.ToString().ToLower()}?startDate={dateRange.StartDate:yyyy-MM-dd}&endDate={dateRange.EndDate:yyyy-MM-dd}";
                if (!string.IsNullOrEmpty(additionalFilter))
                    url += $"&filter={additionalFilter}";

                return await _httpClient.GetFromJsonAsync<ChartData>(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar dados do gráfico: {ex.Message}");
                return CreateMockChartData(reportType);
            }
        }

        public async Task<byte[]> ExportReportAsync(ReportType reportType, DateRange dateRange, ReportExportFormat format)
        {
            try
            {
                var url = $"{_apiUrl}/export/{reportType.ToString().ToLower()}?startDate={dateRange.StartDate:yyyy-MM-dd}&endDate={dateRange.EndDate:yyyy-MM-dd}&format={format.ToString().ToLower()}";
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsByteArrayAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao exportar relatório: {ex.Message}");
                throw;
            }
        }

        #region Métodos de geração de dados de teste
        // IMPORTANTE: Estes métodos só devem ser usados em desenvolvimento ou quando houver falha na API
        private IncomeExpenseReportResult CreateMockIncomeExpenseReport(DateRange dateRange)
        {
            return new IncomeExpenseReportResult
            {
                TotalIncome = 15600.00m,
                TotalExpenses = 10350.45m,
                NetCashflow = 5249.55m,
                SavingsRate = 33.65,
                TopIncomeCategories = new List<CategorySummary>
                {
                    new CategorySummary { Id = "c1", Name = "Salário", Amount = 13500.00m, Percentage = 86.54, Color = "#4CAF50", Type = TransactionType.Income },
                    new CategorySummary { Id = "c2", Name = "Freela", Amount = 2100.00m, Percentage = 13.46, Color = "#8BC34A", Type = TransactionType.Income }
                },
                TopExpenseCategories = new List<CategorySummary>
                {
                    new CategorySummary { Id = "c3", Name = "Moradia", Amount = 3600.00m, Percentage = 34.78, Color = "#9C27B0", Type = TransactionType.Expense },
                    new CategorySummary { Id = "c4", Name = "Alimentação", Amount = 2100.00m, Percentage = 20.29, Color = "#FF5722", Type = TransactionType.Expense },
                    new CategorySummary { Id = "c5", Name = "Transporte", Amount = 900.00m, Percentage = 8.70, Color = "#1976D2", Type = TransactionType.Expense },
                    new CategorySummary { Id = "c6", Name = "Lazer", Amount = 1050.45m, Percentage = 10.15, Color = "#673AB7", Type = TransactionType.Expense },
                    new CategorySummary { Id = "c7", Name = "Serviços", Amount = 1200.00m, Percentage = 11.59, Color = "#E53935", Type = TransactionType.Expense },
                    new CategorySummary { Id = "c8", Name = "Educação", Amount = 1500.00m, Percentage = 14.49, Color = "#009688", Type = TransactionType.Expense }
                },
                IncomeVsExpenseChart = CreateMockIncomeVsExpenseChart(),
                MonthlyTrendChart = CreateMockMonthlyTrendChart(),
                MonthlySummaries = CreateMockMonthlySummaries()
            };
        }

        private CategoryDistributionReportResult CreateMockCategoryDistributionReport(DateRange dateRange, TransactionType type)
        {
            if (type == TransactionType.Income)
            {
                return new CategoryDistributionReportResult
                {
                    Type = type,
                    Total = 15600.00m,
                    Categories = new List<CategorySummary>
                    {
                        new CategorySummary { Id = "c1", Name = "Salário", Amount = 13500.00m, Percentage = 86.54, Color = "#4CAF50", Type = TransactionType.Income },
                        new CategorySummary { Id = "c2", Name = "Freela", Amount = 2100.00m, Percentage = 13.46, Color = "#8BC34A", Type = TransactionType.Income }
                    },
                    PieChart = CreateMockCategoryPieChart(type),
                    TrendChart = CreateMockCategoryTrendChart(type)
                };
            }
            else
            {
                return new CategoryDistributionReportResult
                {
                    Type = type,
                    Total = 10350.45m,
                    Categories = new List<CategorySummary>
                    {
                        new CategorySummary { Id = "c3", Name = "Moradia", Amount = 3600.00m, Percentage = 34.78, Color = "#9C27B0", Type = TransactionType.Expense },
                        new CategorySummary { Id = "c4", Name = "Alimentação", Amount = 2100.00m, Percentage = 20.29, Color = "#FF5722", Type = TransactionType.Expense },
                        new CategorySummary { Id = "c5", Name = "Transporte", Amount = 900.00m, Percentage = 8.70, Color = "#1976D2", Type = TransactionType.Expense },
                        new CategorySummary { Id = "c6", Name = "Lazer", Amount = 1050.45m, Percentage = 10.15, Color = "#673AB7", Type = TransactionType.Expense },
                        new CategorySummary { Id = "c7", Name = "Serviços", Amount = 1200.00m, Percentage = 11.59, Color = "#E53935", Type = TransactionType.Expense },
                        new CategorySummary { Id = "c8", Name = "Educação", Amount = 1500.00m, Percentage = 14.49, Color = "#009688", Type = TransactionType.Expense }
                    },
                    PieChart = CreateMockCategoryPieChart(type),
                    TrendChart = CreateMockCategoryTrendChart(type)
                };
            }
        }

        private CashflowReportResult CreateMockCashflowReport(DateRange dateRange)
        {
            return new CashflowReportResult
            {
                NetCashflow = 5249.55m,
                MonthlyCashflow = CreateMockMonthlySummaries(),
                CashflowChart = CreateMockCashflowChart(),
                AverageMonthlyIncome = 5200.00m,
                AverageMonthlyExpenses = 3450.08m,
                AverageNetCashflow = 1749.92m
            };
        }

        private NetWorthReportResult CreateMockNetWorthReport(DateRange dateRange)
        {
            return new NetWorthReportResult
            {
                TotalAssets = 62587.92m,
                TotalLiabilities = 12000.00m,
                NetWorth = 50587.92m,
                Assets = new List<AccountBalance>
                {
                    new AccountBalance { Id = "1", Name = "Conta Corrente", Type = "Checking", Balance = 4587.92m, Percentage = 7.33 },
                    new AccountBalance { Id = "2", Name = "Poupança", Type = "Savings", Balance = 8000.00m, Percentage = 12.78 },
                    new AccountBalance { Id = "3", Name = "Fundo de Emergência", Type = "Savings", Balance = 15000.00m, Percentage = 23.97 },
                    new AccountBalance { Id = "4", Name = "Investimentos", Type = "Investment", Balance = 35000.00m, Percentage = 55.92 }
                },
                Liabilities = new List<AccountBalance>
                {
                    new AccountBalance { Id = "5", Name = "Cartão de Crédito", Type = "CreditCard", Balance = 2000.00m, Percentage = 16.67 },
                    new AccountBalance { Id = "6", Name = "Financiamento", Type = "Loan", Balance = 10000.00m, Percentage = 83.33 }
                },
                History = CreateMockNetWorthHistory(),
                NetWorthChart = CreateMockNetWorthChart(),
                NetWorthChange = 3500.00m,
                NetWorthChangePercentage = 7.44
            };
        }

        private AccountBalanceHistoryReportResult CreateMockAccountBalanceHistoryReport(string accountId, DateRange dateRange)
        {
            if (accountId == "1") // Conta Corrente
            {
                return new AccountBalanceHistoryReportResult
                {
                    AccountId = "1",
                    AccountName = "Conta Corrente",
                    CurrentBalance = 4587.92m,
                    StartingBalance = 2500.00m,
                    BalanceChange = 2087.92m,
                    BalanceChangePercentage = 83.52,
                    History = CreateMockBalanceHistory(2500.00m),
                    BalanceChart = CreateMockBalanceHistoryChart("Conta Corrente"),
                    AverageBalance = 3543.96m,
                    HighestBalance = 5100.00m,
                    LowestBalance = 2500.00m
                };
            }
            else // Poupança
            {
                return new AccountBalanceHistoryReportResult
                {
                    AccountId = "2",
                    AccountName = "Poupança",
                    CurrentBalance = 8000.00m,
                    StartingBalance = 7500.00m,
                    BalanceChange = 500.00m,
                    BalanceChangePercentage = 6.67,
                    History = CreateMockBalanceHistory(7500.00m),
                    BalanceChart = CreateMockBalanceHistoryChart("Poupança"),
                    AverageBalance = 7750.00m,
                    HighestBalance = 8000.00m,
                    LowestBalance = 7500.00m
                };
            }
        }

        private BudgetPerformanceReportResult CreateMockBudgetPerformanceReport(DateRange dateRange)
        {
            return new BudgetPerformanceReportResult
            {
                TotalBudgeted = 5500.00m,
                TotalSpent = 4650.45m,
                RemainingBudget = 849.55m,
                OverallPerformance = 84.55,
                Categories = new List<BudgetCategorySummary>
                {
                    new BudgetCategorySummary
                    {
                        Id = "b1",
                        Name = "Alimentação",
                        Budgeted = 1000.00m,
                        Spent = 750.45m,
                        Remaining = 249.55m,
                        PercentUsed = 75.05,
                        Status = "Em andamento",
                        Color = "#FF5722"
                    },
                    new BudgetCategorySummary
                    {
                        Id = "b2",
                        Name = "Transporte",
                        Budgeted = 600.00m,
                        Spent = 350.00m,
                        Remaining = 250.00m,
                        PercentUsed = 58.33,
                        Status = "Em andamento",
                        Color = "#1976D2"
                    },
                    new BudgetCategorySummary
                    {
                        Id = "b3",
                        Name = "Lazer",
                        Budgeted = 500.00m,
                        Spent = 350.00m,
                        Remaining = 150.00m,
                        PercentUsed = 70.00,
                        Status = "Em andamento",
                        Color = "#673AB7"
                    },
                    new BudgetCategorySummary
                    {
                        Id = "b4",
                        Name = "Moradia",
                        Budgeted = 3400.00m,
                        Spent = 3200.00m,
                        Remaining = 200.00m,
                        PercentUsed = 94.12,
                        Status = "Em andamento",
                        Color = "#9C27B0"
                    }
                },
                PerformanceChart = CreateMockBudgetPerformanceChart(),
                TrendChart = CreateMockBudgetTrendChart()
            };
        }

        private List<TransactionReportItem> CreateMockTransactionReport(TransactionReportFilter filter)
        {
            var transactions = new List<TransactionReportItem>
            {
                new TransactionReportItem
                {
                    Id = "t1",
                    Description = "Salário",
                    Amount = 4500.00m,
                    Date = DateTime.Today.AddDays(-10),
                    Type = TransactionType.Income,
                    Category = "Renda",
                    CategoryId = "c1",
                    Account = "Conta Corrente",
                    AccountId = "1",
                    IsPending = false,
                    Notes = "Salário mensal",
                    Tags = new List<string> { "Essencial", "Fixo" }
                },
                new TransactionReportItem
                {
                    Id = "t2",
                    Description = "Supermercado",
                    Amount = 350.45m,
                    Date = DateTime.Today.AddDays(-8),
                    Type = TransactionType.Expense,
                    Category = "Alimentação",
                    CategoryId = "c4",
                    Account = "Conta Corrente",
                    AccountId = "1",
                    IsPending = false,
                    Notes = "Compras da semana",
                    Tags = new List<string> { "Essencial" }
                },
                new TransactionReportItem
                {
                    Id = "t3",
                    Description = "Aluguel",
                    Amount = 1200.00m,
                    Date = DateTime.Today.AddDays(-5),
                    Type = TransactionType.Expense,
                    Category = "Moradia",
                    CategoryId = "c3",
                    Account = "Conta Corrente",
                    AccountId = "1",
                    IsPending = false,
                    Notes = "Aluguel mensal",
                    Tags = new List<string> { "Essencial", "Fixo" }
                },
                new TransactionReportItem
                {
                    Id = "t4",
                    Description = "Freela Design",
                    Amount = 700.00m,
                    Date = DateTime.Today.AddDays(-3),
                    Type = TransactionType.Income,
                    Category = "Freela",
                    CategoryId = "c2",
                    Account = "Conta Corrente",
                    AccountId = "1",
                    IsPending = false,
                    Notes = "Projeto de design para cliente",
                    Tags = new List<string> { "Variável" }
                },
                new TransactionReportItem
                {
                    Id = "t5",
                    Description = "Cinema",
                    Amount = 100.00m,
                    Date = DateTime.Today.AddDays(-2),
                    Type = TransactionType.Expense,
                    Category = "Lazer",
                    CategoryId = "c6",
                    Account = "Conta Corrente",
                    AccountId = "1",
                    IsPending = false,
                    Notes = "Filme com amigos",
                    Tags = new List<string> { "Não essencial" }
                }
            };

            // Aplicar filtros básicos como tipo de transação e intervalo de data
            if (filter.TransactionTypes.Any())
            {
                transactions = transactions.Where(t => filter.TransactionTypes.Contains(t.Type)).ToList();
            }

            if (filter.MinAmount.HasValue)
            {
                transactions = transactions.Where(t => t.Amount >= filter.MinAmount.Value).ToList();
            }

            if (filter.MaxAmount.HasValue)
            {
                transactions = transactions.Where(t => t.Amount <= filter.MaxAmount.Value).ToList();
            }

            if (!string.IsNullOrEmpty(filter.SearchTerm))
            {
                var searchTerm = filter.SearchTerm.ToLower();
                transactions = transactions.Where(t => 
                    t.Description.ToLower().Contains(searchTerm) || 
                    t.Notes?.ToLower().Contains(searchTerm) == true || 
                    t.Category.ToLower().Contains(searchTerm) ||
                    t.Account.ToLower().Contains(searchTerm) ||
                    (t.Tags != null && t.Tags.Any(tag => tag.ToLower().Contains(searchTerm)))
                ).ToList();
            }

            // Simular ordenação
            if (!string.IsNullOrEmpty(filter.SortBy))
            {
                switch (filter.SortBy.ToLower())
                {
                    case "date":
                        transactions = filter.SortAscending
                            ? transactions.OrderBy(t => t.Date).ToList()
                            : transactions.OrderByDescending(t => t.Date).ToList();
                        break;
                    case "amount":
                        transactions = filter.SortAscending
                            ? transactions.OrderBy(t => t.Amount).ToList()
                            : transactions.OrderByDescending(t => t.Amount).ToList();
                        break;
                    case "description":
                        transactions = filter.SortAscending
                            ? transactions.OrderBy(t => t.Description).ToList()
                            : transactions.OrderByDescending(t => t.Description).ToList();
                        break;
                }
            }

            // Simular paginação
            int skip = (filter.Page - 1) * filter.PageSize;
            transactions = transactions.Skip(skip).Take(filter.PageSize).ToList();

            return transactions;
        }

        private ExpensesTrendReportResult CreateMockExpensesTrendReport(DateRange dateRange)
        {
            return new ExpensesTrendReportResult
            {
                MonthlyExpenses = new List<MonthlyCategoryAmount>
                {
                    new MonthlyCategoryAmount
                    {
                        Year = DateTime.Today.Year,
                        Month = 1,
                        MonthName = "Jan",
                        TotalAmount = 3300.00m,
                        Categories = new List<CategoryAmount>
                        {
                            new CategoryAmount { Id = "c3", Name = "Moradia", Amount = 1200.00m, Percentage = 36.36, Color = "#9C27B0" },
                            new CategoryAmount { Id = "c4", Name = "Alimentação", Amount = 700.00m, Percentage = 21.21, Color = "#FF5722" },
                            new CategoryAmount { Id = "c5", Name = "Transporte", Amount = 300.00m, Percentage = 9.09, Color = "#1976D2" },
                            new CategoryAmount { Id = "c6", Name = "Lazer", Amount = 350.00m, Percentage = 10.61, Color = "#673AB7" },
                            new CategoryAmount { Id = "c7", Name = "Serviços", Amount = 400.00m, Percentage = 12.12, Color = "#E53935" },
                            new CategoryAmount { Id = "c8", Name = "Educação", Amount = 350.00m, Percentage = 10.61, Color = "#009688" }
                        }
                    },
                    new MonthlyCategoryAmount
                    {
                        Year = DateTime.Today.Year,
                        Month = 2,
                        MonthName = "Fev",
                        TotalAmount = 3700.00m,
                        Categories = new List<CategoryAmount>
                        {
                            new CategoryAmount { Id = "c3", Name = "Moradia", Amount = 1200.00m, Percentage = 32.43, Color = "#9C27B0" },
                            new CategoryAmount { Id = "c4", Name = "Alimentação", Amount = 800.00m, Percentage = 21.62, Color = "#FF5722" },
                            new CategoryAmount { Id = "c5", Name = "Transporte", Amount = 350.00m, Percentage = 9.46, Color = "#1976D2" },
                            new CategoryAmount { Id = "c6", Name = "Lazer", Amount = 400.00m, Percentage = 10.81, Color = "#673AB7" },
                            new CategoryAmount { Id = "c7", Name = "Serviços", Amount = 400.00m, Percentage = 10.81, Color = "#E53935" },
                            new CategoryAmount { Id = "c8", Name = "Educação", Amount = 550.00m, Percentage = 14.86, Color = "#009688" }
                        }
                    },
                    new MonthlyCategoryAmount
                    {
                        Year = DateTime.Today.Year,
                        Month = 3,
                        MonthName = "Mar",
                        TotalAmount = 3300.00m,
                        Categories = new List<CategoryAmount>
                        {
                            new CategoryAmount { Id = "c3", Name = "Moradia", Amount = 1200.00m, Percentage = 36.36, Color = "#9C27B0" },
                            new CategoryAmount { Id = "c4", Name = "Alimentação", Amount = 650.00m, Percentage = 19.70, Color = "#FF5722" },
                            new CategoryAmount { Id = "c5", Name = "Transporte", Amount = 300.00m, Percentage = 9.09, Color = "#1976D2" },
                            new CategoryAmount { Id = "c6", Name = "Lazer", Amount = 300.00m, Percentage = 9.09, Color = "#673AB7" },
                            new CategoryAmount { Id = "c7", Name = "Serviços", Amount = 400.00m, Percentage = 12.12, Color = "#E53935" },
                            new CategoryAmount { Id = "c8", Name = "Educação", Amount = 450.00m, Percentage = 13.64, Color = "#009688" }
                        }
                    },
                    new MonthlyCategoryAmount
                    {
                        Year = DateTime.Today.Year,
                        Month = 4,
                        MonthName = "Abr",
                        TotalAmount = 3600.00m,
                        Categories = new List<CategoryAmount>
                        {
                            new CategoryAmount { Id = "c3", Name = "Moradia", Amount = 1200.00m, Percentage = 33.33, Color = "#9C27B0" },
                            new CategoryAmount { Id = "c4", Name = "Alimentação", Amount = 750.00m, Percentage = 20.83, Color = "#FF5722" },
                            new CategoryAmount { Id = "c5", Name = "Transporte", Amount = 300.00m, Percentage = 8.33, Color = "#1976D2" },
                            new CategoryAmount { Id = "c6", Name = "Lazer", Amount = 450.00m, Percentage = 12.50, Color = "#673AB7" },
                            new CategoryAmount { Id = "c7", Name = "Serviços", Amount = 400.00m, Percentage = 11.11, Color = "#E53935" },
                            new CategoryAmount { Id = "c8", Name = "Educação", Amount = 500.00m, Percentage = 13.89, Color = "#009688" }
                        }
                    },
                    new MonthlyCategoryAmount
                    {
                        Year = DateTime.Today.Year,
                        Month = 5,
                        MonthName = "Mai",
                        TotalAmount = 3200.00m,
                        Categories = new List<CategoryAmount>
                        {
                            new CategoryAmount { Id = "c3", Name = "Moradia", Amount = 1200.00m, Percentage = 37.50, Color = "#9C27B0" },
                            new CategoryAmount { Id = "c4", Name = "Alimentação", Amount = 650.00m, Percentage = 20.31, Color = "#FF5722" },
                            new CategoryAmount { Id = "c5", Name = "Transporte", Amount = 250.00m, Percentage = 7.81, Color = "#1976D2" },
                            new CategoryAmount { Id = "c6", Name = "Lazer", Amount = 300.00m, Percentage = 9.38, Color = "#673AB7" },
                            new CategoryAmount { Id = "c7", Name = "Serviços", Amount = 350.00m, Percentage = 10.94, Color = "#E53935" },
                            new CategoryAmount { Id = "c8", Name = "Educação", Amount = 450.00m, Percentage = 14.06, Color = "#009688" }
                        }
                    },
                    new MonthlyCategoryAmount
                    {
                        Year = DateTime.Today.Year,
                        Month = 6,
                        MonthName = "Jun",
                        TotalAmount = 3450.45m,
                        Categories = new List<CategoryAmount>
                        {
                            new CategoryAmount { Id = "c3", Name = "Moradia", Amount = 1200.00m, Percentage = 34.78, Color = "#9C27B0" },
                            new CategoryAmount { Id = "c4", Name = "Alimentação", Amount = 750.45m, Percentage = 21.75, Color = "#FF5722" },
                            new CategoryAmount { Id = "c5", Name = "Transporte", Amount = 250.00m, Percentage = 7.25, Color = "#1976D2" },
                            new CategoryAmount { Id = "c6", Name = "Lazer", Amount = 350.00m, Percentage = 10.14, Color = "#673AB7" },
                            new CategoryAmount { Id = "c7", Name = "Serviços", Amount = 400.00m, Percentage = 11.59, Color = "#E53935" },
                            new CategoryAmount { Id = "c8", Name = "Educação", Amount = 500.00m, Percentage = 14.49, Color = "#009688" }
                        }
                    }
                },
                TrendChart = CreateMockExpensesTrendChart(),
                AverageMonthlyExpense = 3425.08m,
                HighestMonthlyExpense = 3700.00m,
                LowestMonthlyExpense = 3200.00m,
                HighestExpenseCategory = new CategorySummary { Id = "c3", Name = "Moradia", Amount = 7200.00m, Percentage = 35.00, Color = "#9C27B0", Type = TransactionType.Expense }
            };
        }

        private IncomeTrendReportResult CreateMockIncomeTrendReport(DateRange dateRange)
        {
            return new IncomeTrendReportResult
            {
                MonthlyIncome = new List<MonthlyCategoryAmount>
                {
                    new MonthlyCategoryAmount
                    {
                        Year = DateTime.Today.Year,
                        Month = 1,
                        MonthName = "Jan",
                        TotalAmount = 4800.00m,
                        Categories = new List<CategoryAmount>
                        {
                            new CategoryAmount { Id = "c1", Name = "Salário", Amount = 4500.00m, Percentage = 93.75, Color = "#4CAF50" },
                            new CategoryAmount { Id = "c2", Name = "Freela", Amount = 300.00m, Percentage = 6.25, Color = "#8BC34A" }
                        }
                    },
                    new MonthlyCategoryAmount
                    {
                        Year = DateTime.Today.Year,
                        Month = 2,
                        MonthName = "Fev",
                        TotalAmount = 4900.00m,
                        Categories = new List<CategoryAmount>
                        {
                            new CategoryAmount { Id = "c1", Name = "Salário", Amount = 4500.00m, Percentage = 91.84, Color = "#4CAF50" },
                            new CategoryAmount { Id = "c2", Name = "Freela", Amount = 400.00m, Percentage = 8.16, Color = "#8BC34A" }
                        }
                    },
                    new MonthlyCategoryAmount
                    {
                        Year = DateTime.Today.Year,
                        Month = 3,
                        MonthName = "Mar",
                        TotalAmount = 5100.00m,
                        Categories = new List<CategoryAmount>
                        {
                            new CategoryAmount { Id = "c1", Name = "Salário", Amount = 4500.00m, Percentage = 88.24, Color = "#4CAF50" },
                            new CategoryAmount { Id = "c2", Name = "Freela", Amount = 600.00m, Percentage = 11.76, Color = "#8BC34A" }
                        }
                    },
                    new MonthlyCategoryAmount
                    {
                        Year = DateTime.Today.Year,
                        Month = 4,
                        MonthName = "Abr",
                        TotalAmount = 4700.00m,
                        Categories = new List<CategoryAmount>
                        {
                            new CategoryAmount { Id = "c1", Name = "Salário", Amount = 4500.00m, Percentage = 95.74, Color = "#4CAF50" },
                            new CategoryAmount { Id = "c2", Name = "Freela", Amount = 200.00m, Percentage = 4.26, Color = "#8BC34A" }
                        }
                    },
                    new MonthlyCategoryAmount
                    {
                        Year = DateTime.Today.Year,
                        Month = 5,
                        MonthName = "Mai",
                        TotalAmount = 5200.00m,
                        Categories = new List<CategoryAmount>
                        {
                            new CategoryAmount { Id = "c1", Name = "Salário", Amount = 4500.00m, Percentage = 86.54, Color = "#4CAF50" },
                            new CategoryAmount { Id = "c2", Name = "Freela", Amount = 700.00m, Percentage = 13.46, Color = "#8BC34A" }
                        }
                    },
                    new MonthlyCategoryAmount
                    {
                        Year = DateTime.Today.Year,
                        Month = 6,
                        MonthName = "Jun",
                        TotalAmount = 5200.00m,
                        Categories = new List<CategoryAmount>
                        {
                            new CategoryAmount { Id = "c1", Name = "Salário", Amount = 4500.00m, Percentage = 86.54, Color = "#4CAF50" },
                            new CategoryAmount { Id = "c2", Name = "Freela", Amount = 700.00m, Percentage = 13.46, Color = "#8BC34A" }
                        }
                    }
                },
                TrendChart = CreateMockIncomeTrendChart(),
                AverageMonthlyIncome = 4983.33m,
                HighestMonthlyIncome = 5200.00m,
                LowestMonthlyIncome = 4700.00m,
                HighestIncomeCategory = new CategorySummary { Id = "c1", Name = "Salário", Amount = 27000.00m, Percentage = 90.45, Color = "#4CAF50", Type = TransactionType.Income }
            };
        }

        private SavingsRateReportResult CreateMockSavingsRateReport(DateRange dateRange)
        {
            return new SavingsRateReportResult
            {
                MonthlySavingsRates = new List<MonthlySavingsRate>
                {
                    new MonthlySavingsRate { Year = DateTime.Today.Year, Month = 1, MonthName = "Jan", Income = 4800.00m, Expenses = 3300.00m, Saved = 1500.00m, SavingsRate = 31.25 },
                    new MonthlySavingsRate { Year = DateTime.Today.Year, Month = 2, MonthName = "Fev", Income = 4900.00m, Expenses = 3700.00m, Saved = 1200.00m, SavingsRate = 24.49 },
                    new MonthlySavingsRate { Year = DateTime.Today.Year, Month = 3, MonthName = "Mar", Income = 5100.00m, Expenses = 3300.00m, Saved = 1800.00m, SavingsRate = 35.29 },
                    new MonthlySavingsRate { Year = DateTime.Today.Year, Month = 4, MonthName = "Abr", Income = 4700.00m, Expenses = 3600.00m, Saved = 1100.00m, SavingsRate = 23.40 },
                    new MonthlySavingsRate { Year = DateTime.Today.Year, Month = 5, MonthName = "Mai", Income = 5200.00m, Expenses = 3200.00m, Saved = 2000.00m, SavingsRate = 38.46 },
                    new MonthlySavingsRate { Year = DateTime.Today.Year, Month = 6, MonthName = "Jun", Income = 5200.00m, Expenses = 3450.45m, Saved = 1749.55m, SavingsRate = 33.65 }
                },
                AverageSavingsRate = 31.09,
                HighestSavingsRate = 38.46,
                LowestSavingsRate = 23.40,
                SavingsRateChart = CreateMockSavingsRateChart()
            };
        }

        private ChartData CreateMockChartData(ReportType reportType)
        {
            switch (reportType)
            {
                case ReportType.IncomeExpense:
                    return CreateMockIncomeVsExpenseChart();
                case ReportType.CategoryDistribution:
                    return CreateMockCategoryPieChart(TransactionType.Expense);
                case ReportType.Cashflow:
                    return CreateMockCashflowChart();
                case ReportType.NetWorth:
                    return CreateMockNetWorthChart();
                case ReportType.ExpensesTrend:
                    return CreateMockExpensesTrendChart();
                case ReportType.IncomeTrend:
                    return CreateMockIncomeTrendChart();
                case ReportType.SavingsRate:
                    return CreateMockSavingsRateChart();
                case ReportType.BudgetPerformance:
                    return CreateMockBudgetPerformanceChart();
                default:
                    return new ChartData
                    {
                        Title = "Dados não disponíveis",
                        Labels = new List<string> { "Sem dados" },
                        Datasets = new List<ChartDataset>
                        {
                            new ChartDataset
                            {
                                Label = "Sem dados",
                                Data = new List<decimal> { 0 },
                                BackgroundColor = new List<string> { "#CCCCCC" },
                                BorderColor = new List<string> { "#AAAAAA" }
                            }
                        }
                    };
            }
        }

        private ChartData CreateMockIncomeVsExpenseChart()
        {
            return new ChartData
            {
                Title = "Receitas e Despesas",
                Labels = new List<string> { "Jan", "Fev", "Mar", "Abr", "Mai", "Jun" },
                Datasets = new List<ChartDataset>
                {
                    new ChartDataset
                    {
                        Label = "Receitas",
                        Data = new List<decimal> { 4800, 4900, 5100, 4700, 5200, 5200 },
                        BackgroundColor = new List<string> { "rgba(76, 175, 80, 0.2)" },
                        BorderColor = new List<string> { "#4CAF50" },
                        BorderWidth = 2
                    },
                    new ChartDataset
                    {
                        Label = "Despesas",
                        Data = new List<decimal> { 3300, 3700, 3300, 3600, 3200, 3450.45m },
                        BackgroundColor = new List<string> { "rgba(244, 67, 54, 0.2)" },
                        BorderColor = new List<string> { "#F44336" },
                        BorderWidth = 2
                    }
                }
            };
        }

        private ChartData CreateMockMonthlyTrendChart()
        {
            return new ChartData
            {
                Title = "Tendência Mensal",
                Labels = new List<string> { "Jan", "Fev", "Mar", "Abr", "Mai", "Jun" },
                Datasets = new List<ChartDataset>
                {
                    new ChartDataset
                    {
                        Label = "Receitas",
                        Data = new List<decimal> { 4800, 4900, 5100, 4700, 5200, 5200 },
                        BackgroundColor = new List<string> { "rgba(76, 175, 80, 0.2)" },
                        BorderColor = new List<string> { "#4CAF50" },
                        Type = "line",
                        BorderWidth = 2
                    },
                    new ChartDataset
                    {
                        Label = "Despesas",
                        Data = new List<decimal> { 3300, 3700, 3300, 3600, 3200, 3450.45m },
                        BackgroundColor = new List<string> { "rgba(244, 67, 54, 0.2)" },
                        BorderColor = new List<string> { "#F44336" },
                        Type = "line",
                        BorderWidth = 2
                    },
                    new ChartDataset
                    {
                        Label = "Saldo",
                        Data = new List<decimal> { 1500, 1200, 1800, 1100, 2000, 1749.55m },
                        BackgroundColor = new List<string> { "rgba(33, 150, 243, 0.2)" },
                        BorderColor = new List<string> { "#2196F3" },
                        Type = "bar",
                        BorderWidth = 2
                    }
                }
            };
        }

        private List<MonthlySummary> CreateMockMonthlySummaries()
        {
            return new List<MonthlySummary>
            {
                new MonthlySummary { Year = DateTime.Today.Year, Month = 1, MonthName = "Jan", Income = 4800.00m, Expenses = 3300.00m, NetCashflow = 1500.00m, SavingsRate = 31.25 },
                new MonthlySummary { Year = DateTime.Today.Year, Month = 2, MonthName = "Fev", Income = 4900.00m, Expenses = 3700.00m, NetCashflow = 1200.00m, SavingsRate = 24.49 },
                new MonthlySummary { Year = DateTime.Today.Year, Month = 3, MonthName = "Mar", Income = 5100.00m, Expenses = 3300.00m, NetCashflow = 1800.00m, SavingsRate = 35.29 },
                new MonthlySummary { Year = DateTime.Today.Year, Month = 4, MonthName = "Abr", Income = 4700.00m, Expenses = 3600.00m, NetCashflow = 1100.00m, SavingsRate = 23.40 },
                new MonthlySummary { Year = DateTime.Today.Year, Month = 5, MonthName = "Mai", Income = 5200.00m, Expenses = 3200.00m, NetCashflow = 2000.00m, SavingsRate = 38.46 },
                new MonthlySummary { Year = DateTime.Today.Year, Month = 6, MonthName = "Jun", Income = 5200.00m, Expenses = 3450.45m, NetCashflow = 1749.55m, SavingsRate = 33.65 }
            };
        }

        private ChartData CreateMockCategoryPieChart(TransactionType type)
        {
            var title = type == TransactionType.Income ? "Distribuição de Receitas" : "Distribuição de Despesas";
            var colors = new List<string> { "#FF5722", "#9C27B0", "#673AB7", "#3F51B5", "#2196F3", "#009688" };
            var borderColors = new List<string> { "#FF5722", "#9C27B0", "#673AB7", "#3F51B5", "#2196F3", "#009688" };

            if (type == TransactionType.Income)
            {
                return new ChartData
                {
                    Title = title,
                    Labels = new List<string> { "Salário", "Freela" },
                    Datasets = new List<ChartDataset>
                    {
                        new ChartDataset
                        {
                            Label = "Receitas",
                            Data = new List<decimal> { 13500, 2100 },
                            BackgroundColor = new List<string> { "#4CAF50", "#8BC34A" },
                            BorderColor = new List<string> { "#4CAF50", "#8BC34A" },
                            BorderWidth = 2
                        }
                    }
                };
            }
            else
            {
                return new ChartData
                {
                    Title = title,
                    Labels = new List<string> { "Moradia", "Alimentação", "Transporte", "Lazer", "Serviços", "Educação" },
                    Datasets = new List<ChartDataset>
                    {
                        new ChartDataset
                        {
                            Label = "Despesas",
                            Data = new List<decimal> { 3600.00m, 2100.00m, 900.00m, 1050.45m, 1200.00m, 1500.00m },
                            BackgroundColor = colors,
                            BorderColor = borderColors,
                            BorderWidth = 2
                        }
                    }
                };
            }
        }

        private ChartData CreateMockCategoryTrendChart(TransactionType type)
        {
            if (type == TransactionType.Income)
            {
                return new ChartData
                {
                    Title = "Tendência de Receitas por Categoria",
                    Labels = new List<string> { "Jan", "Fev", "Mar", "Abr", "Mai", "Jun" },
                    Datasets = new List<ChartDataset>
                    {
                        new ChartDataset
                        {
                            Label = "Salário",
                            Data = new List<decimal> { 4500, 4500, 4500, 4500, 4500, 4500 },
                            BackgroundColor = new List<string> { "rgba(76, 175, 80, 0.2)" },
                            BorderColor = new List<string> { "#4CAF50" },
                            Type = "line",
                            BorderWidth = 2
                        },
                        new ChartDataset
                        {
                            Label = "Freela",
                            Data = new List<decimal> { 300, 400, 600, 200, 700, 700 },
                            BackgroundColor = new List<string> { "rgba(139, 195, 74, 0.2)" },
                            BorderColor = new List<string> { "#8BC34A" },
                            Type = "line",
                            BorderWidth = 2
                        }
                    }
                };
            }
            else
            {
                return new ChartData
                {
                    Title = "Tendência de Despesas por Categoria",
                    Labels = new List<string> { "Jan", "Fev", "Mar", "Abr", "Mai", "Jun" },
                    Datasets = new List<ChartDataset>
                    {
                        new ChartDataset
                        {
                            Label = "Moradia",
                            Data = new List<decimal> { 1200, 1200, 1200, 1200, 1200, 1200 },
                            BackgroundColor = new List<string> { "rgba(156, 39, 176, 0.2)" },
                            BorderColor = new List<string> { "#9C27B0" },
                            Type = "line",
                            BorderWidth = 2
                        },
                        new ChartDataset
                        {
                            Label = "Alimentação",
                            Data = new List<decimal> { 700, 800, 650, 750, 650, 750.45m },
                            BackgroundColor = new List<string> { "rgba(255, 87, 34, 0.2)" },
                            BorderColor = new List<string> { "#FF5722" },
                            Type = "line",
                            BorderWidth = 2
                        },
                        new ChartDataset
                        {
                            Label = "Transporte",
                            Data = new List<decimal> { 300, 350, 300, 300, 250, 250 },
                            BackgroundColor = new List<string> { "rgba(25, 118, 210, 0.2)" },
                            BorderColor = new List<string> { "#1976D2" },
                            Type = "line",
                            BorderWidth = 2
                        }
                    }
                };
            }
        }

        private ChartData CreateMockCashflowChart()
        {
            return new ChartData
            {
                Title = "Fluxo de Caixa",
                Labels = new List<string> { "Jan", "Fev", "Mar", "Abr", "Mai", "Jun" },
                Datasets = new List<ChartDataset>
                {
                    new ChartDataset
                    {
                        Label = "Receitas",
                        Data = new List<decimal> { 4800, 4900, 5100, 4700, 5200, 5200 },
                        BackgroundColor = new List<string> { "rgba(76, 175, 80, 0.2)" },
                        BorderColor = new List<string> { "#4CAF50" },
                        Type = "bar",
                        BorderWidth = 2
                    },
                    new ChartDataset
                    {
                        Label = "Despesas",
                        Data = new List<decimal> { 3300, 3700, 3300, 3600, 3200, 3450.45m },
                        BackgroundColor = new List<string> { "rgba(244, 67, 54, 0.2)" },
                        BorderColor = new List<string> { "#F44336" },
                        Type = "bar",
                        BorderWidth = 2
                    },
                    new ChartDataset
                    {
                        Label = "Saldo Líquido",
                        Data = new List<decimal> { 1500, 1200, 1800, 1100, 2000, 1749.55m },
                        BackgroundColor = new List<string> { "rgba(33, 150, 243, 0.2)" },
                        BorderColor = new List<string> { "#2196F3" },
                        Type = "line",
                        BorderWidth = 2
                    }
                }
            };
        }

        private List<NetWorthHistory> CreateMockNetWorthHistory()
        {
            var today = DateTime.Today;
            var result = new List<NetWorthHistory>();

            for (int i = 0; i < 6; i++)
            {
                var date = new DateTime(today.Year, today.Month, 1).AddMonths(-5 + i);
                var assets = 56000 + i * 1500;
                var liabilities = 13000 - i * 200;
                var netWorth = assets - liabilities;

                result.Add(new NetWorthHistory
                {
                    Date = date,
                    Assets = assets,
                    Liabilities = liabilities,
                    NetWorth = netWorth
                });
            }

            return result;
        }

        private ChartData CreateMockNetWorthChart()
        {
            return new ChartData
            {
                Title = "Evolução do Patrimônio Líquido",
                Labels = new List<string> { "Jan", "Fev", "Mar", "Abr", "Mai", "Jun" },
                Datasets = new List<ChartDataset>
                {
                    new ChartDataset
                    {
                        Label = "Ativos",
                        Data = new List<decimal> { 56000, 57500, 59000, 60500, 62000, 63500 },
                        BackgroundColor = new List<string> { "rgba(76, 175, 80, 0.2)" },
                        BorderColor = new List<string> { "#4CAF50" },
                        Type = "line",
                        BorderWidth = 2
                    },
                    new ChartDataset
                    {
                        Label = "Passivos",
                        Data = new List<decimal> { 13000, 12800, 12600, 12400, 12200, 12000 },
                        BackgroundColor = new List<string> { "rgba(244, 67, 54, 0.2)" },
                        BorderColor = new List<string> { "#F44336" },
                        Type = "line",
                        BorderWidth = 2
                    },
                    new ChartDataset
                    {
                        Label = "Patrimônio Líquido",
                        Data = new List<decimal> { 43000, 44700, 46400, 48100, 49800, 51500 },
                        BackgroundColor = new List<string> { "rgba(33, 150, 243, 0.2)" },
                        BorderColor = new List<string> { "#2196F3" },
                        Type = "line",
                        BorderWidth = 3
                    }
                }
            };
        }

        private List<BalanceHistory> CreateMockBalanceHistory(decimal startingBalance)
        {
            var result = new List<BalanceHistory>();
            var today = DateTime.Today;
            var balance = startingBalance;

            for (int i = 0; i < 31; i++)
            {
                var date = new DateTime(today.Year, today.Month, 1).AddDays(i);
                if (date > today)
                    break;

                var change = (i % 5 == 0) ? RandomChange() : 0;
                balance += change;

                result.Add(new BalanceHistory
                {
                    Date = date,
                    Balance = balance,
                    Change = change
                });
            }

            return result;
        }

        private ChartData CreateMockBalanceHistoryChart(string accountName)
        {
            return new ChartData
            {
                Title = $"Histórico de Saldo - {accountName}",
                Labels = new List<string> { "1", "5", "10", "15", "20", "25", "30" },
                Datasets = new List<ChartDataset>
                {
                    new ChartDataset
                    {
                        Label = "Saldo",
                        Data = accountName == "Conta Corrente"
                            ? new List<decimal> { 2500, 3200, 3800, 4200, 3900, 4300, 4587.92m }
                            : new List<decimal> { 7500, 7550, 7600, 7650, 7800, 7900, 8000 },
                        BackgroundColor = new List<string> { accountName == "Conta Corrente" ? "rgba(30, 136, 229, 0.2)" : "rgba(67, 160, 71, 0.2)" },
                        BorderColor = new List<string> { accountName == "Conta Corrente" ? "#1E88E5" : "#43A047" },
                        Type = "line",
                        BorderWidth = 2,
                        Fill = true
                    }
                }
            };
        }

        private ChartData CreateMockBudgetPerformanceChart()
        {
            return new ChartData
            {
                Title = "Desempenho do Orçamento",
                Labels = new List<string> { "Alimentação", "Transporte", "Lazer", "Moradia" },
                Datasets = new List<ChartDataset>
                {
                    new ChartDataset
                    {
                        Label = "Orçado",
                        Data = new List<decimal> { 1000, 600, 500, 3400 },
                        BackgroundColor = new List<string> { "rgba(33, 150, 243, 0.2)", "rgba(33, 150, 243, 0.2)", "rgba(33, 150, 243, 0.2)", "rgba(33, 150, 243, 0.2)" },
                        BorderColor = new List<string> { "#2196F3", "#2196F3", "#2196F3", "#2196F3" },
                        Type = "bar",
                        BorderWidth = 2
                    },
                    new ChartDataset
                    {
                        Label = "Gasto",
                        Data = new List<decimal> { 750.45m, 350, 350, 3200 },
                        BackgroundColor = new List<string> { "rgba(244, 67, 54, 0.2)", "rgba(244, 67, 54, 0.2)", "rgba(244, 67, 54, 0.2)", "rgba(244, 67, 54, 0.2)" },
                        BorderColor = new List<string> { "#F44336", "#F44336", "#F44336", "#F44336" },
                        Type = "bar",
                        BorderWidth = 2
                    }
                }
            };
        }

        private ChartData CreateMockBudgetTrendChart()
        {
            return new ChartData
            {
                Title = "Tendência de Desempenho do Orçamento",
                Labels = new List<string> { "Jan", "Fev", "Mar", "Abr", "Mai", "Jun" },
                Datasets = new List<ChartDataset>
                {
                    new ChartDataset
                    {
                        Label = "Orçado",
                        Data = new List<decimal> { 5500, 5500, 5500, 5500, 5500, 5500 },
                        BackgroundColor = new List<string> { "rgba(33, 150, 243, 0.2)" },
                        BorderColor = new List<string> { "#2196F3" },
                        Type = "line",
                        BorderWidth = 2
                    },
                    new ChartDataset
                    {
                        Label = "Gasto",
                        Data = new List<decimal> { 4200, 4700, 4300, 4800, 4500, 4650.45m },
                        BackgroundColor = new List<string> { "rgba(244, 67, 54, 0.2)" },
                        BorderColor = new List<string> { "#F44336" },
                        Type = "line",
                        BorderWidth = 2
                    }
                }
            };
        }

        private ChartData CreateMockExpensesTrendChart()
        {
            return new ChartData
            {
                Title = "Tendência de Despesas por Mês",
                Labels = new List<string> { "Jan", "Fev", "Mar", "Abr", "Mai", "Jun" },
                Datasets = new List<ChartDataset>
                {
                    new ChartDataset
                    {
                        Label = "Moradia",
                        Data = new List<decimal> { 1200, 1200, 1200, 1200, 1200, 1200 },
                        BackgroundColor = new List<string> { "#9C27B0" },
                        BorderColor = new List<string> { "#9C27B0" },
                        Type = "bar",
                        BorderWidth = 1
                    },
                    new ChartDataset
                    {
                        Label = "Alimentação",
                        Data = new List<decimal> { 700, 800, 650, 750, 650, 750.45m },
                        BackgroundColor = new List<string> { "#FF5722" },
                        BorderColor = new List<string> { "#FF5722" },
                        Type = "bar",
                        BorderWidth = 1
                    },
                    new ChartDataset
                    {
                        Label = "Transporte",
                        Data = new List<decimal> { 300, 350, 300, 300, 250, 250 },
                        BackgroundColor = new List<string> { "#1976D2" },
                        BorderColor = new List<string> { "#1976D2" },
                        Type = "bar",
                        BorderWidth = 1
                    },
                    new ChartDataset
                    {
                        Label = "Lazer",
                        Data = new List<decimal> { 350, 400, 300, 450, 300, 350 },
                        BackgroundColor = new List<string> { "#673AB7" },
                        BorderColor = new List<string> { "#673AB7" },
                        Type = "bar",
                        BorderWidth = 1
                    },
                    new ChartDataset
                    {
                        Label = "Serviços",
                        Data = new List<decimal> { 400, 400, 400, 400, 350, 400 },
                        BackgroundColor = new List<string> { "#E53935" },
                        BorderColor = new List<string> { "#E53935" },
                        Type = "bar",
                        BorderWidth = 1
                    },
                    new ChartDataset
                    {
                        Label = "Educação",
                        Data = new List<decimal> { 350, 550, 450, 500, 450, 500 },
                        BackgroundColor = new List<string> { "#009688" },
                        BorderColor = new List<string> { "#009688" },
                        Type = "bar",
                        BorderWidth = 1
                    },
                    new ChartDataset
                    {
                        Label = "Total",
                        Data = new List<decimal> { 3300, 3700, 3300, 3600, 3200, 3450.45m },
                        BackgroundColor = new List<string> { "rgba(0, 0, 0, 0)" },
                        BorderColor = new List<string> { "#333333" },
                        Type = "line",
                        BorderWidth = 2
                    }
                }
            };
        }

        private ChartData CreateMockIncomeTrendChart()
        {
            return new ChartData
            {
                Title = "Tendência de Receitas por Mês",
                Labels = new List<string> { "Jan", "Fev", "Mar", "Abr", "Mai", "Jun" },
                Datasets = new List<ChartDataset>
                {
                    new ChartDataset
                    {
                        Label = "Salário",
                        Data = new List<decimal> { 4500, 4500, 4500, 4500, 4500, 4500 },
                        BackgroundColor = new List<string> { "#4CAF50" },
                        BorderColor = new List<string> { "#4CAF50" },
                        Type = "bar",
                        BorderWidth = 1
                    },
                    new ChartDataset
                    {
                        Label = "Freela",
                        Data = new List<decimal> { 300, 400, 600, 200, 700, 700 },
                        BackgroundColor = new List<string> { "#8BC34A" },
                        BorderColor = new List<string> { "#8BC34A" },
                        Type = "bar",
                        BorderWidth = 1
                    },
                    new ChartDataset
                    {
                        Label = "Total",
                        Data = new List<decimal> { 4800, 4900, 5100, 4700, 5200, 5200 },
                        BackgroundColor = new List<string> { "rgba(0, 0, 0, 0)" },
                        BorderColor = new List<string> { "#333333" },
                        Type = "line",
                        BorderWidth = 2
                    }
                }
            };
        }

        private ChartData CreateMockSavingsRateChart()
        {
            return new ChartData
            {
                Title = "Taxa de Poupança Mensal",
                Labels = new List<string> { "Jan", "Fev", "Mar", "Abr", "Mai", "Jun" },
                Datasets = new List<ChartDataset>
                {
                    new ChartDataset
                    {
                        Label = "Taxa de Poupança (%)",
                        Data = new List<decimal> { 31.25m, 24.49m, 35.29m, 23.40m, 38.46m, 33.65m },
                        BackgroundColor = new List<string> { "rgba(0, 150, 136, 0.2)" },
                        BorderColor = new List<string> { "#009688" },
                        Type = "line",
                        BorderWidth = 2,
                        Fill = true
                    }
                }
            };
        }

        private decimal RandomChange()
        {
            var random = new Random();
            var amount = random.Next(1, 10) * 100.00m;
            return random.Next(0, 2) == 0 ? -amount : amount;
        }
        #endregion
    }
}