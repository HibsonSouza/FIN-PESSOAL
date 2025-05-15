using FinanceManager.ClientApp.Models;

namespace FinanceManager.ClientApp.Services.Interfaces
{
    public interface ICreditCardService
    {
        Task<List<CreditCardViewModel>> GetCreditCards();
        Task<CreditCardViewModel> GetCreditCardById(string id);
        Task<CreditCardViewModel> GetCreditCardByIdAsync(string id);
        Task<bool> CreateCreditCard(CreditCardCreateModel creditCard);
        Task<bool> UpdateCreditCard(string id, CreditCardUpdateModel creditCard);
        Task<bool> DeleteCreditCard(string id);
        Task<List<CreditCardTransactionViewModel>> GetCreditCardTransactions(string creditCardId, DateTimeRange dateRange);
        Task<bool> AddCreditCardTransaction(string creditCardId, CreditCardTransactionCreateModel transaction);
    }
}