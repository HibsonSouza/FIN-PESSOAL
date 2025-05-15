using FinanceManager.ClientApp.Models;
using FinanceManager.ClientApp.Services.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

namespace FinanceManager.ClientApp.Services
{
    public class ReportService : IReportService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiEndpoint = "api/reports";
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public ReportService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<FinancialSummaryModel> GetFinancialSummary(DateTimeRange dateRange)
        {
            try
            {
                var response = await _httpClient.GetAsync(
                    $"{_apiEndpoint}/financial-summary?startDate={dateRange.Start:yyyy-MM-dd}&endDate={dateRange.End:yyyy-MM-dd}");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<FinancialSummaryModel>(_jsonOptions) 
                        ?? new FinancialSummaryModel();
                }
                
                return new FinancialSummaryModel();
            }
            catch
            {
                return new FinancialSummaryModel();
            }
        }

        public async Task<CategoryBreakdownModel> GetCategoryBreakdown(DateTimeRange dateRange, TransactionType transactionType)
        {
            try
            {
                var response = await _httpClient.GetAsync(
                    $"{_apiEndpoint}/category-breakdown?startDate={dateRange.Start:yyyy-MM-dd}&endDate={dateRange.End:yyyy-MM-dd}&type={transactionType}");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<CategoryBreakdownModel>(_jsonOptions) 
                        ?? new CategoryBreakdownModel();
                }
                
                return new CategoryBreakdownModel();
            }
            catch
            {
                return new CategoryBreakdownModel();
            }
        }

        public async Task<MonthlyFinancialDataModel> GetMonthlyFinancialData(int year)
        {
            try
            {
                var response = await _httpClient.GetAsync(
                    $"{_apiEndpoint}/monthly-data?year={year}");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<MonthlyFinancialDataModel>(_jsonOptions) 
                        ?? new MonthlyFinancialDataModel();
                }
                
                return new MonthlyFinancialDataModel();
            }
            catch
            {
                return new MonthlyFinancialDataModel();
            }
        }

        public async Task<CashFlowReportModel> GetCashFlowReport(DateTimeRange dateRange)
        {
            try
            {
                var response = await _httpClient.GetAsync(
                    $"{_apiEndpoint}/cash-flow?startDate={dateRange.Start:yyyy-MM-dd}&endDate={dateRange.End:yyyy-MM-dd}");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<CashFlowReportModel>(_jsonOptions) 
                        ?? new CashFlowReportModel();
                }
                
                return new CashFlowReportModel();
            }
            catch
            {
                return new CashFlowReportModel();
            }
        }

        public async Task<ChartData> GetExpensesByCategoryChart(DateTimeRange dateRange)
        {
            try
            {
                var response = await _httpClient.GetAsync(
                    $"{_apiEndpoint}/expenses-chart?startDate={dateRange.Start:yyyy-MM-dd}&endDate={dateRange.End:yyyy-MM-dd}");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ChartData>(_jsonOptions) 
                        ?? new ChartData();
                }
                
                return new ChartData();
            }
            catch
            {
                return new ChartData();
            }
        }

        public async Task<ChartData> GetIncomeByCategoryChart(DateTimeRange dateRange)
        {
            try
            {
                var response = await _httpClient.GetAsync(
                    $"{_apiEndpoint}/income-chart?startDate={dateRange.Start:yyyy-MM-dd}&endDate={dateRange.End:yyyy-MM-dd}");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ChartData>(_jsonOptions) 
                        ?? new ChartData();
                }
                
                return new ChartData();
            }
            catch
            {
                return new ChartData();
            }
        }

        public async Task<NetWorthReportModel> GetNetWorthReport(DateTimeRange dateRange)
        {
            try
            {
                var response = await _httpClient.GetAsync(
                    $"{_apiEndpoint}/net-worth?startDate={dateRange.Start:yyyy-MM-dd}&endDate={dateRange.End:yyyy-MM-dd}");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<NetWorthReportModel>(_jsonOptions) 
                        ?? new NetWorthReportModel();
                }
                
                return new NetWorthReportModel();
            }
            catch
            {
                return new NetWorthReportModel();
            }
        }

        public async Task<AccountBalanceReportModel> GetAccountBalanceReport(string accountId, DateTimeRange dateRange)
        {
            try
            {
                var response = await _httpClient.GetAsync(
                    $"{_apiEndpoint}/account-balance/{accountId}?startDate={dateRange.Start:yyyy-MM-dd}&endDate={dateRange.End:yyyy-MM-dd}");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<AccountBalanceReportModel>(_jsonOptions) 
                        ?? new AccountBalanceReportModel();
                }
                
                return new AccountBalanceReportModel();
            }
            catch
            {
                return new AccountBalanceReportModel();
            }
        }

        public async Task<ExpenseTrendReportResult> GetExpensesTrend(DateTimeRange dateRange, string? categoryId = null)
        {
            try
            {
                var url = $"{_apiEndpoint}/expenses-trend?startDate={dateRange.Start:yyyy-MM-dd}&endDate={dateRange.End:yyyy-MM-dd}";
                if (!string.IsNullOrEmpty(categoryId))
                {
                    url += $"&categoryId={categoryId}";
                }
                
                var response = await _httpClient.GetAsync(url);
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ExpenseTrendReportResult>(_jsonOptions) 
                        ?? new ExpenseTrendReportResult();
                }
                
                return new ExpenseTrendReportResult();
            }
            catch
            {
                return new ExpenseTrendReportResult();
            }
        }

        public async Task<IncomeTrendReportResult> GetIncomeTrend(DateTimeRange dateRange, string? categoryId = null)
        {
            try
            {
                var url = $"{_apiEndpoint}/income-trend?startDate={dateRange.Start:yyyy-MM-dd}&endDate={dateRange.End:yyyy-MM-dd}";
                if (!string.IsNullOrEmpty(categoryId))
                {
                    url += $"&categoryId={categoryId}";
                }
                
                var response = await _httpClient.GetAsync(url);
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<IncomeTrendReportResult>(_jsonOptions) 
                        ?? new IncomeTrendReportResult();
                }
                
                return new IncomeTrendReportResult();
            }
            catch
            {
                return new IncomeTrendReportResult();
            }
        }

        public async Task<SavingsRateReportResult> GetSavingsRateReport(DateTimeRange dateRange)
        {
            try
            {
                var response = await _httpClient.GetAsync(
                    $"{_apiEndpoint}/savings-rate?startDate={dateRange.Start:yyyy-MM-dd}&endDate={dateRange.End:yyyy-MM-dd}");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<SavingsRateReportResult>(_jsonOptions) 
                        ?? new SavingsRateReportResult();
                }
                
                return new SavingsRateReportResult();
            }
            catch
            {
                return new SavingsRateReportResult();
            }
        }

        public async Task<DashboardSummary> GetDashboardSummary()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiEndpoint}/dashboard-summary");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<DashboardSummary>(_jsonOptions) 
                        ?? new DashboardSummary();
                }
                
                return new DashboardSummary();
            }
            catch
            {
                return new DashboardSummary();
            }
        }
    }
}