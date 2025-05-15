using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinanceManager.ClientApp.Models;

namespace FinanceManager.ClientApp.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<List<TransactionViewModel>> GetTransactionsAsync(TransactionFilterModel filter = null);
        Task<TransactionViewModel> GetTransactionByIdAsync(int id);
        Task<int> CreateTransactionAsync(TransactionCreateModel transaction);
        Task<bool> UpdateTransactionAsync(int id, TransactionUpdateModel transaction);
        Task<bool> DeleteTransactionAsync(int id);
        Task<bool> BulkUpdateTransactionsAsync(TransactionBulkUpdateModel bulkUpdate);
        Task<TransactionSummaryViewModel> GetTransactionSummaryAsync(DateRange dateRange, List<int> accountIds = null);
        Task<List<TransactionViewModel>> GetTransactionsAsync(DateTime? startDate = null, DateTime? endDate = null);
        Task<List<MonthlyFinancialSummary>> GetMonthlySummaryAsync(int year, int numberOfMonths);
        Task<FinancialFlowViewModel> GetFinancialFlowAsync(int numberOfMonths);
        Task<List<string>> GetAllTagsAsync();
        Task<bool> ImportTransactionsAsync(ImportTransactionsModel importModel);
    }
}