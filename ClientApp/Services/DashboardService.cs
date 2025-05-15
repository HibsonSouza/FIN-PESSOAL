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
    public class DashboardService : IDashboardService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "api/dashboard";

        public DashboardService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DashboardViewModel> GetDashboardDataAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<DashboardViewModel>($"{_apiUrl}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar dados do dashboard: {ex.Message}");
                return CreateMockDashboardData();
            }
        }

        public async Task<DashboardViewModel> GetDashboardDataAsync(DateRange dateRange)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<DashboardViewModel>(
                    $"{_apiUrl}?startDate={dateRange.StartDate:yyyy-MM-dd}&endDate={dateRange.EndDate:yyyy-MM-dd}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar dados do dashboard: {ex.Message}");
                return CreateMockDashboardData();
            }
        }

        public async Task<List<TransactionSummaryViewModel>> GetRecentTransactionsAsync(int count = 5)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<TransactionSummaryViewModel>>($"{_apiUrl}/recent-transactions?count={count}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar transações recentes: {ex.Message}");
                return new List<TransactionSummaryViewModel>();
            }
        }

        public async Task<decimal> GetTotalBalanceAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<decimal>($"{_apiUrl}/total-balance");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar saldo total: {ex.Message}");
                return 0;
            }
        }

        public async Task<ChartData> GetIncomeExpenseChartDataAsync(DateRange dateRange)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ChartData>(
                    $"{_apiUrl}/income-expense-chart?startDate={dateRange.StartDate:yyyy-MM-dd}&endDate={dateRange.EndDate:yyyy-MM-dd}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar dados do gráfico de receitas e despesas: {ex.Message}");
                return CreateMockIncomeExpenseChartData();
            }
        }

        public async Task<ChartData> GetCategoryDistributionChartDataAsync(DateRange dateRange, TransactionType type)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ChartData>(
                    $"{_apiUrl}/category-distribution-chart?startDate={dateRange.StartDate:yyyy-MM-dd}&endDate={dateRange.EndDate:yyyy-MM-dd}&type={type}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar dados do gráfico de distribuição por categoria: {ex.Message}");
                return CreateMockCategoryDistributionChartData(type);
            }
        }

        public async Task<ChartData> GetAccountBalancesChartDataAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ChartData>($"{_apiUrl}/account-balances-chart");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar dados do gráfico de saldos de contas: {ex.Message}");
                return CreateMockAccountBalancesChartData();
            }
        }

        #region Métodos de geração de dados de teste
        // IMPORTANTE: Estes métodos só devem ser usados em desenvolvimento ou quando houver falha na API
        private DashboardViewModel CreateMockDashboardData()
        {
            // Cria dados de teste consistentes para desenvolvimento
            var today = DateTime.Today;
            var startDate = new DateTime(today.Year, today.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            return new DashboardViewModel
            {
                TotalBalance = 12587.92m,
                TotalIncome = 5200.00m,
                TotalExpense = 3450.45m,
                NetCashflow = 1749.55m,
                AccountBalances = new List<AccountBalanceViewModel>
                {
                    new AccountBalanceViewModel
                    {
                        Id = "1",
                        Name = "Conta Corrente",
                        Balance = 4587.92m,
                        Type = "Checking",
                        Color = "#1E88E5",
                        Icon = "account_balance",
                        IsActive = true
                    },
                    new AccountBalanceViewModel
                    {
                        Id = "2",
                        Name = "Poupança",
                        Balance = 8000.00m,
                        Type = "Savings",
                        Color = "#43A047",
                        Icon = "savings",
                        IsActive = true
                    }
                },
                RecentTransactions = new List<TransactionSummaryViewModel>
                {
                    new TransactionSummaryViewModel
                    {
                        Id = "t1",
                        Description = "Salário",
                        Amount = 4500.00m,
                        Date = DateTime.Today.AddDays(-1),
                        Type = TransactionType.Income,
                        Category = "Renda",
                        CategoryIcon = "payments",
                        CategoryColor = "#43A047",
                        Account = "Conta Corrente"
                    },
                    new TransactionSummaryViewModel
                    {
                        Id = "t2",
                        Description = "Supermercado",
                        Amount = 350.45m,
                        Date = DateTime.Today.AddDays(-2),
                        Type = TransactionType.Expense,
                        Category = "Alimentação",
                        CategoryIcon = "restaurant",
                        CategoryColor = "#FF5722",
                        Account = "Conta Corrente"
                    },
                    new TransactionSummaryViewModel
                    {
                        Id = "t3",
                        Description = "Aluguel",
                        Amount = 1200.00m,
                        Date = DateTime.Today.AddDays(-5),
                        Type = TransactionType.Expense,
                        Category = "Moradia",
                        CategoryIcon = "home",
                        CategoryColor = "#9C27B0",
                        Account = "Conta Corrente"
                    }
                },
                BudgetProgress = new List<BudgetProgressViewModel>
                {
                    new BudgetProgressViewModel
                    {
                        Id = "b1",
                        Name = "Alimentação",
                        Amount = 1000.00m,
                        Spent = 750.45m,
                        Available = 249.55m,
                        PercentUsed = 75.05,
                        Category = "Alimentação",
                        CategoryIcon = "restaurant",
                        CategoryColor = "#FF5722",
                        Period = BudgetPeriod.Monthly
                    },
                    new BudgetProgressViewModel
                    {
                        Id = "b2",
                        Name = "Transporte",
                        Amount = 600.00m,
                        Spent = 350.00m,
                        Available = 250.00m,
                        PercentUsed = 58.33,
                        Category = "Transporte",
                        CategoryIcon = "directions_car",
                        CategoryColor = "#1976D2",
                        Period = BudgetPeriod.Monthly
                    },
                    new BudgetProgressViewModel
                    {
                        Id = "b3",
                        Name = "Lazer",
                        Amount = 500.00m,
                        Spent = 350.00m,
                        Available = 150.00m,
                        PercentUsed = 70.00,
                        Category = "Lazer",
                        CategoryIcon = "sports_esports",
                        CategoryColor = "#673AB7",
                        Period = BudgetPeriod.Monthly
                    }
                },
                UpcomingBills = new List<UpcomingBillViewModel>
                {
                    new UpcomingBillViewModel
                    {
                        Id = "u1",
                        Description = "Aluguel - Junho",
                        Amount = 1200.00m,
                        DueDate = DateTime.Today.AddDays(5),
                        IsPaid = false,
                        IsRecurring = true,
                        Category = "Moradia",
                        CategoryIcon = "home",
                        CategoryColor = "#9C27B0",
                        DaysUntilDue = 5
                    },
                    new UpcomingBillViewModel
                    {
                        Id = "u2",
                        Description = "Internet",
                        Amount = 120.00m,
                        DueDate = DateTime.Today.AddDays(7),
                        IsPaid = false,
                        IsRecurring = true,
                        Category = "Serviços",
                        CategoryIcon = "wifi",
                        CategoryColor = "#E53935",
                        DaysUntilDue = 7
                    }
                },
                PendingIncome = 700.00m,
                PendingExpenses = 1320.00m,
                GoalsCount = 3,
                CompletedGoalsCount = 1,
                IncomeExpenseChart = CreateMockIncomeExpenseChartData(),
                CategoryDistributionChart = CreateMockCategoryDistributionChartData(TransactionType.Expense),
                CashflowTrendChart = CreateMockCashflowTrendChartData(),
                Alerts = new List<FinancialAlertViewModel>
                {
                    new FinancialAlertViewModel
                    {
                        Id = "alert1",
                        Title = "Orçamento próximo do limite",
                        Message = "Seu orçamento de Alimentação está 75% utilizado.",
                        Type = AlertType.Warning,
                        CreatedAt = DateTime.Today,
                        IsRead = false,
                        ActionLink = "/budgets/b1",
                        ActionText = "Ver orçamento"
                    },
                    new FinancialAlertViewModel
                    {
                        Id = "alert2",
                        Title = "Fatura do cartão vence em breve",
                        Message = "A fatura do seu cartão de crédito vence em 3 dias.",
                        Type = AlertType.Info,
                        CreatedAt = DateTime.Today,
                        IsRead = false,
                        ActionLink = "/creditcards/statement/1",
                        ActionText = "Ver fatura"
                    }
                }
            };
        }

        private ChartData CreateMockIncomeExpenseChartData()
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
                        Data = new List<decimal> { 3500, 3700, 3300, 3600, 3200, 3450 },
                        BackgroundColor = new List<string> { "rgba(244, 67, 54, 0.2)" },
                        BorderColor = new List<string> { "#F44336" },
                        BorderWidth = 2
                    }
                }
            };
        }

        private ChartData CreateMockCategoryDistributionChartData(TransactionType type)
        {
            var title = type == TransactionType.Income ? "Distribuição de Receitas" : "Distribuição de Despesas";
            var colors = new List<string> { "#FF5722", "#9C27B0", "#673AB7", "#3F51B5", "#2196F3", "#009688" };
            var borderColors = new List<string> { "#FF5722", "#9C27B0", "#673AB7", "#3F51B5", "#2196F3", "#009688" };

            if (type == TransactionType.Income)
            {
                return new ChartData
                {
                    Title = title,
                    Labels = new List<string> { "Salário", "Freela", "Dividendos", "Outros" },
                    Datasets = new List<ChartDataset>
                    {
                        new ChartDataset
                        {
                            Label = "Receitas",
                            Data = new List<decimal> { 4500, 700, 300, 200 },
                            BackgroundColor = new List<string> { "#4CAF50", "#8BC34A", "#CDDC39", "#FFEB3B" },
                            BorderColor = new List<string> { "#4CAF50", "#8BC34A", "#CDDC39", "#FFEB3B" },
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
                    Labels = new List<string> { "Alimentação", "Moradia", "Transporte", "Serviços", "Lazer", "Educação" },
                    Datasets = new List<ChartDataset>
                    {
                        new ChartDataset
                        {
                            Label = "Despesas",
                            Data = new List<decimal> { 750.45m, 1200, 350, 320, 350, 480 },
                            BackgroundColor = colors,
                            BorderColor = borderColors,
                            BorderWidth = 2
                        }
                    }
                };
            }
        }

        private ChartData CreateMockCashflowTrendChartData()
        {
            return new ChartData
            {
                Title = "Fluxo de Caixa",
                Labels = new List<string> { "Jan", "Fev", "Mar", "Abr", "Mai", "Jun" },
                Datasets = new List<ChartDataset>
                {
                    new ChartDataset
                    {
                        Label = "Saldo Líquido",
                        Data = new List<decimal> { 1300, 1200, 1800, 1100, 2000, 1750 },
                        BackgroundColor = new List<string> { "rgba(33, 150, 243, 0.2)" },
                        BorderColor = new List<string> { "#2196F3" },
                        BorderWidth = 2
                    }
                }
            };
        }

        private ChartData CreateMockAccountBalancesChartData()
        {
            return new ChartData
            {
                Title = "Saldos por Conta",
                Labels = new List<string> { "Conta Corrente", "Poupança" },
                Datasets = new List<ChartDataset>
                {
                    new ChartDataset
                    {
                        Label = "Saldo",
                        Data = new List<decimal> { 4587.92m, 8000 },
                        BackgroundColor = new List<string> { "#1E88E5", "#43A047" },
                        BorderColor = new List<string> { "#1E88E5", "#43A047" },
                        BorderWidth = 2
                    }
                }
            };
        }
        #endregion
    }
}