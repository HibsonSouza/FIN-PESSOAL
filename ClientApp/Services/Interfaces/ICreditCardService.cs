using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinanceManager.ClientApp.Models;

namespace FinanceManager.ClientApp.Services.Interfaces
{
    public interface ICreditCardService
    {
        Task<List<CreditCardViewModel>> GetCreditCardsAsync();
        Task<CreditCardViewModel> GetCreditCardByIdAsync(int id);
        Task<CreditCardViewModel> CreateCreditCardAsync(CreditCardCreateModel creditCard);
        Task<CreditCardViewModel> UpdateCreditCardAsync(int id, CreditCardUpdateModel creditCard);
        Task<bool> DeleteCreditCardAsync(int id);
        Task<CreditCardStatementModel> GetCurrentStatementAsync(int id);
        Task<List<CreditCardStatementViewModel>> GetStatementsHistoryAsync(int id, int count = 6);
        Task<List<TransactionViewModel>> GetCreditCardTransactionsAsync(int id, DateTime? startDate = null, DateTime? endDate = null);
    }
}