using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FinanceManager.ClientApp.Models;
using FinanceManager.ClientApp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace FinanceManager.ClientApp.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<TransactionService> _logger;

        public TransactionService(HttpClient httpClient, ILogger<TransactionService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<TransactionViewModel>> GetTransactionsAsync(TransactionFilterModel filter = null)
        {
            try
            {
                // Implementação real usaria API
                // return await _httpClient.GetFromJsonAsync<List<TransactionViewModel>>("api/transactions");
                
                // Dados de exemplo para desenvolvimento
                return new List<TransactionViewModel>
                {
                    new TransactionViewModel
                    {
                        Id = 1,
                        Description = "Salário",
                        Amount = 3500,
                        Date = DateTime.Now.AddDays(-5),
                        Type = TransactionType.Income,
                        CategoryName = "Salário",
                        AccountName = "Conta Corrente",
                        IsReconciled = true
                    },
                    new TransactionViewModel
                    {
                        Id = 2,
                        Description = "Supermercado",
                        Amount = 250.75m,
                        Date = DateTime.Now.AddDays(-2),
                        Type = TransactionType.Expense,
                        CategoryName = "Alimentação",
                        AccountName = "Conta Corrente",
                        IsReconciled = true
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter transações");
                return new List<TransactionViewModel>();
            }
        }

        public async Task<TransactionViewModel> GetTransactionByIdAsync(int id)
        {
            try
            {
                // Implementação real usaria API
                // return await _httpClient.GetFromJsonAsync<TransactionViewModel>($"api/transactions/{id}");
                
                // Dados de exemplo
                return new TransactionViewModel
                {
                    Id = id,
                    Description = "Transação de exemplo",
                    Amount = 100,
                    Date = DateTime.Now,
                    Type = TransactionType.Expense,
                    CategoryName = "Diversos",
                    AccountName = "Conta Corrente"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter transação com ID {Id}", id);
                return null;
            }
        }

        public async Task<int> CreateTransactionAsync(TransactionCreateModel transaction)
        {
            try
            {
                // Implementação real usaria API
                // var response = await _httpClient.PostAsJsonAsync("api/transactions", transaction);
                // response.EnsureSuccessStatusCode();
                // var result = await response.Content.ReadFromJsonAsync<int>();
                // return result;
                
                // Simulando criação
                return new Random().Next(100, 999);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar transação");
                throw;
            }
        }

        public async Task<bool> UpdateTransactionAsync(int id, TransactionUpdateModel transaction)
        {
            try
            {
                // Implementação real usaria API
                // var response = await _httpClient.PutAsJsonAsync($"api/transactions/{id}", transaction);
                // return response.IsSuccessStatusCode;
                
                // Simulando atualização
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar transação com ID {Id}", id);
                return false;
            }
        }

        public async Task<bool> DeleteTransactionAsync(int id)
        {
            try
            {
                // Implementação real usaria API
                // var response = await _httpClient.DeleteAsync($"api/transactions/{id}");
                // return response.IsSuccessStatusCode;
                
                // Simulando exclusão
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao excluir transação com ID {Id}", id);
                return false;
            }
        }

        public async Task<bool> BulkUpdateTransactionsAsync(TransactionBulkUpdateModel bulkUpdate)
        {
            try
            {
                // Implementação real usaria API
                // var response = await _httpClient.PutAsJsonAsync("api/transactions/bulk", bulkUpdate);
                // return response.IsSuccessStatusCode;
                
                // Simulando atualização em massa
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar transações em massa");
                return false;
            }
        }

        public async Task<TransactionSummaryViewModel> GetTransactionSummaryAsync(DateRange dateRange, List<int> accountIds = null)
        {
            try
            {
                // Implementação real usaria API
                // return await _httpClient.GetFromJsonAsync<TransactionSummaryViewModel>($"api/transactions/summary?start={dateRange.Start}&end={dateRange.End}");
                
                // Dados de exemplo
                return new TransactionSummaryViewModel
                {
                    TotalIncome = 4500,
                    TotalExpense = 3200,
                    TopExpenseCategories = new List<CategorySummary>
                    {
                        new CategorySummary { CategoryName = "Moradia", Amount = 1500, Percentage = 46.87 },
                        new CategorySummary { CategoryName = "Alimentação", Amount = 800, Percentage = 25.00 },
                        new CategorySummary { CategoryName = "Transporte", Amount = 400, Percentage = 12.50 },
                        new CategorySummary { CategoryName = "Lazer", Amount = 300, Percentage = 9.38 },
                        new CategorySummary { CategoryName = "Outros", Amount = 200, Percentage = 6.25 }
                    },
                    TopIncomeCategories = new List<CategorySummary>
                    {
                        new CategorySummary { CategoryName = "Salário", Amount = 3500, Percentage = 77.78 },
                        new CategorySummary { CategoryName = "Freelance", Amount = 800, Percentage = 17.78 },
                        new CategorySummary { CategoryName = "Outros", Amount = 200, Percentage = 4.44 }
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter resumo de transações");
                return new TransactionSummaryViewModel();
            }
        }

        public async Task<List<TransactionViewModel>> GetTransactionsAsync(DateTime? startDate = null, DateTime? endDate = null)
        {
            try
            {
                // Implementação real usaria API
                string query = "";
                if (startDate.HasValue)
                    query += $"start={startDate.Value.ToString("yyyy-MM-dd")}";
                
                if (endDate.HasValue)
                {
                    if (!string.IsNullOrEmpty(query))
                        query += "&";
                    query += $"end={endDate.Value.ToString("yyyy-MM-dd")}";
                }

                // return await _httpClient.GetFromJsonAsync<List<TransactionViewModel>>($"api/transactions?{query}");
                
                // Dados de exemplo
                return await GetTransactionsAsync(null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter transações por período");
                return new List<TransactionViewModel>();
            }
        }

        public async Task<List<MonthlyFinancialSummary>> GetMonthlySummaryAsync(int year, int numberOfMonths)
        {
            try
            {
                // Implementação real usaria API
                // return await _httpClient.GetFromJsonAsync<List<MonthlyFinancialSummary>>($"api/transactions/monthly-summary?year={year}&months={numberOfMonths}");
                
                // Dados de exemplo
                var result = new List<MonthlyFinancialSummary>();
                
                for (int i = 0; i < numberOfMonths; i++)
                {
                    var currentMonth = DateTime.Now.AddMonths(-i);
                    if (currentMonth.Year == year || numberOfMonths > 12)
                    {
                        result.Add(new MonthlyFinancialSummary
                        {
                            Year = currentMonth.Year,
                            Month = currentMonth.Month,
                            TotalIncome = 3500 + (new Random().Next(-500, 500)),
                            TotalExpense = 2800 + (new Random().Next(-300, 300))
                        });
                    }
                }
                
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter resumo mensal para o ano {Year}", year);
                return new List<MonthlyFinancialSummary>();
            }
        }

        public async Task<FinancialFlowViewModel> GetFinancialFlowAsync(int numberOfMonths)
        {
            try
            {
                // Implementação real usaria API
                // return await _httpClient.GetFromJsonAsync<FinancialFlowViewModel>($"api/transactions/financial-flow?months={numberOfMonths}");
                
                // Dados de exemplo
                var monthlySummaries = await GetMonthlySummaryAsync(DateTime.Now.Year, numberOfMonths);
                
                return new FinancialFlowViewModel
                {
                    MonthlySummaries = monthlySummaries,
                    TotalIncome = monthlySummaries.Sum(m => m.TotalIncome),
                    TotalExpense = monthlySummaries.Sum(m => m.TotalExpense)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter fluxo financeiro para {Months} meses", numberOfMonths);
                return new FinancialFlowViewModel();
            }
        }

        public async Task<List<string>> GetAllTagsAsync()
        {
            try
            {
                // Implementação real usaria API
                // return await _httpClient.GetFromJsonAsync<List<string>>("api/transactions/tags");
                
                // Dados de exemplo
                return new List<string>
                {
                    "importante", "parcelado", "essencial", "lazer", "investimento", "emergência"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter todas as tags");
                return new List<string>();
            }
        }

        public async Task<bool> ImportTransactionsAsync(ImportTransactionsModel importModel)
        {
            try
            {
                // Implementação real usaria API
                // var response = await _httpClient.PostAsJsonAsync("api/transactions/import", importModel);
                // return response.IsSuccessStatusCode;
                
                // Simulando importação
                await Task.Delay(1000); // Simula processamento
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao importar transações");
                return false;
            }
        }
    }
}