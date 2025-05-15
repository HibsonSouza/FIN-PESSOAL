using FinanceManager.ClientApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceManager.ClientApp.Services
{
    public interface ICreditCardService
    {
        Task<List<CreditCardViewModel>> GetCardsAsync();
        Task<CreditCardViewModel> GetCardByIdAsync(string id);
        Task<CreditCardViewModel> CreateCardAsync(CreditCardCreateModel card);
        Task<CreditCardViewModel> UpdateCardAsync(string id, CreditCardUpdateModel card);
        Task<bool> DeleteCardAsync(string id);
        Task<bool> AddCardStatementAsync(string cardId, CreditCardStatementModel statement);
        Task<List<CreditCardStatementViewModel>> GetCardStatementsAsync(string cardId);
    }
}