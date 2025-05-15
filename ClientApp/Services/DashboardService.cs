using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FinanceManager.ClientApp.Models;
using FinanceManager.ClientApp.Services.Interfaces;

namespace FinanceManager.ClientApp.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly HttpClient _httpClient;

        public DashboardService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DashboardSummary> GetDashboardSummaryAsync()
        {
            // Em um cenário real, este dado seria obtido da API
            // var response = await _httpClient.GetFromJsonAsync<DashboardSummary>("api/dashboard/summary");
            // return response;
            
            // Por enquanto, retornamos dados simulados
            await Task.Delay(300); // Simula atraso de rede
            
            return new DashboardSummary
            {
                TotalBalance = 15850.75m,
                MonthlyIncome = 8500.00m,
                MonthlyExpenses = 4325.80m,
                IncomeChange = 3.2m,
                ExpenseChange = -1.8m,
                LastUpdated = DateTime.Now
            };
        }

        public async Task<List<RecentTransactionModel>> GetRecentTransactionsAsync(int count)
        {
            // Em um cenário real, este dado seria obtido da API
            // var response = await _httpClient.GetFromJsonAsync<List<RecentTransactionModel>>($"api/transactions/recent?count={count}");
            // return response;
            
            // Por enquanto, retornamos dados simulados
            await Task.Delay(300); // Simula atraso de rede
            
            var transactions = new List<RecentTransactionModel>();
            
            // Exemplo de transações
            transactions.Add(new RecentTransactionModel
            {
                Id = 1,
                Date = DateTime.Now.AddDays(-1),
                Description = "Supermercado Compre Bem",
                Category = "Alimentação",
                Account = "Nuconta",
                Amount = -352.76m
            });
            
            transactions.Add(new RecentTransactionModel
            {
                Id = 2,
                Date = DateTime.Now.AddDays(-2),
                Description = "Salário",
                Category = "Renda",
                Account = "Bradesco",
                Amount = 8500.00m
            });
            
            transactions.Add(new RecentTransactionModel
            {
                Id = 3,
                Date = DateTime.Now.AddDays(-3),
                Description = "Academia",
                Category = "Saúde",
                Account = "Nubank",
                Amount = -99.90m
            });
            
            transactions.Add(new RecentTransactionModel
            {
                Id = 4,
                Date = DateTime.Now.AddDays(-5),
                Description = "Aluguel",
                Category = "Moradia",
                Account = "Bradesco",
                Amount = -1800.00m
            });
            
            transactions.Add(new RecentTransactionModel
            {
                Id = 5,
                Date = DateTime.Now.AddDays(-8),
                Description = "Uber",
                Category = "Transporte",
                Account = "Nubank",
                Amount = -32.50m
            });
            
            return transactions;
        }

        public async Task<List<CategorySummary>> GetCategorySummaryAsync(DateRange dateRange)
        {
            // Em um cenário real, este dado seria obtido da API
            // var response = await _httpClient.PostAsJsonAsync<DateRange>("api/dashboard/categorysummary", dateRange);
            // return await response.Content.ReadFromJsonAsync<List<CategorySummary>>();
            
            // Por enquanto, retornamos dados simulados
            await Task.Delay(300); // Simula atraso de rede
            
            var categories = new List<CategorySummary>();
            
            // Exemplo de dados de categoria
            categories.Add(new CategorySummary
            {
                CategoryId = 1,
                CategoryName = "Alimentação",
                Amount = 1250.50m,
                Percentage = 30,
                Color = "#4CAF50"
            });
            
            categories.Add(new CategorySummary
            {
                CategoryId = 2,
                CategoryName = "Moradia",
                Amount = 1800.00m,
                Percentage = 20,
                Color = "#2196F3"
            });
            
            categories.Add(new CategorySummary
            {
                CategoryId = 3,
                CategoryName = "Transporte",
                Amount = 650.30m,
                Percentage = 15,
                Color = "#FF9800"
            });
            
            categories.Add(new CategorySummary
            {
                CategoryId = 4,
                CategoryName = "Lazer",
                Amount = 425.00m,
                Percentage = 10,
                Color = "#E91E63"
            });
            
            categories.Add(new CategorySummary
            {
                CategoryId = 5,
                CategoryName = "Outros",
                Amount = 1025.75m,
                Percentage = 25,
                Color = "#9C27B0"
            });
            
            return categories;
        }

        public async Task<BalanceForecast> GetBalanceForecastAsync(int months)
        {
            // Em um cenário real, este dado seria obtido da API
            // var response = await _httpClient.GetFromJsonAsync<BalanceForecast>($"api/dashboard/forecast?months={months}");
            // return response;
            
            // Por enquanto, retornamos dados simulados
            await Task.Delay(300); // Simula atraso de rede
            
            var forecast = new BalanceForecast
            {
                Dates = new List<DateTime>(),
                IncomeValues = new List<decimal>(),
                ExpenseValues = new List<decimal>(),
                BalanceValues = new List<decimal>()
            };
            
            var now = DateTime.Now;
            var initialBalance = 15850.75m;
            
            for (int i = 0; i < months; i++)
            {
                var forecastDate = new DateTime(now.Year, now.Month, 1).AddMonths(i);
                forecast.Dates.Add(forecastDate);
                
                var income = 8500m + (i * 200m); // Aumenta a renda em 200 por mês
                var expense = 4325.80m + (i * 150m); // Aumenta os gastos em 150 por mês
                var balance = initialBalance + (income - expense) * (i + 1);
                
                forecast.IncomeValues.Add(income);
                forecast.ExpenseValues.Add(expense);
                forecast.BalanceValues.Add(balance);
            }
            
            return forecast;
        }
    }
}