using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinanceManager.ClientApp.Models;

namespace FinanceManager.ClientApp.Services.Interfaces
{
    public interface IInvestmentService
    {
        Task<List<InvestmentViewModel>> GetInvestmentsAsync();
        Task<InvestmentViewModel> GetInvestmentByIdAsync(string id);
        Task<InvestmentViewModel> CreateInvestmentAsync(InvestmentCreateModel investment);
        Task<InvestmentViewModel> UpdateInvestmentAsync(int id, InvestmentUpdateModel investment);
        Task<bool> DeleteInvestmentAsync(int id);
        Task<List<InvestmentTransactionViewModel>> GetInvestmentTransactionsAsync(int investmentId);
        Task<InvestmentTransactionViewModel> AddInvestmentTransactionAsync(int investmentId, InvestmentTransactionViewModel transaction);
        Task<InvestmentTransactionViewModel> UpdateInvestmentTransactionAsync(string transactionId, InvestmentTransactionFormModel model);
        Task<InvestmentTransactionViewModel> AddInvestmentTransactionAsync(InvestmentTransactionFormModel model);
        Task<bool> DeleteInvestmentTransactionAsync(string transactionId);
        Task<Dictionary<DateTime, decimal>> GetInvestmentPerformanceAsync(int investmentId, DateTime startDate, DateTime endDate);
        Task<decimal> GetTotalInvestmentsValueAsync();
    }
}