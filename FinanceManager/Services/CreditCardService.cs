using FinanceManager.Models;
using FinanceManager.Repositories.Interfaces;

namespace FinanceManager.Services.Interfaces
{
    /// <summary>
    /// Interface para o serviço de cartões de crédito
    /// </summary>
    public interface ICreditCardService
    {
        Task<IEnumerable<CreditCard>> GetAllCreditCardsAsync(int userId);
        Task<CreditCard?> GetCreditCardByIdAsync(int id);
        Task<CreditCard?> GetCreditCardByNumberAsync(string cardNumber);
        Task<CreditCard> CreateCreditCardAsync(CreditCard creditCard);
        Task<CreditCard> UpdateCreditCardAsync(CreditCard creditCard);
        Task DeleteCreditCardAsync(int id);
    }
}

namespace FinanceManager.Services
{
    /// <summary>
    /// Implementação do serviço de cartões de crédito
    /// </summary>
    public class CreditCardService : ICreditCardService
    {
        private readonly ICreditCardRepository _creditCardRepository;

        public CreditCardService(ICreditCardRepository creditCardRepository)
        {
            _creditCardRepository = creditCardRepository;
        }

        public async Task<IEnumerable<CreditCard>> GetAllCreditCardsAsync(int userId)
        {
            return await _creditCardRepository.GetByUserIdAsync(userId);
        }

        public async Task<CreditCard?> GetCreditCardByIdAsync(int id)
        {
            return await _creditCardRepository.GetByIdAsync(id);
        }

        public async Task<CreditCard?> GetCreditCardByNumberAsync(string cardNumber)
        {
            return await _creditCardRepository.GetByNumberAsync(cardNumber);
        }

        public async Task<CreditCard> CreateCreditCardAsync(CreditCard creditCard)
        {
            return await _creditCardRepository.CreateAsync(creditCard);
        }

        public async Task<CreditCard> UpdateCreditCardAsync(CreditCard creditCard)
        {
            return await _creditCardRepository.UpdateAsync(creditCard);
        }

        public async Task DeleteCreditCardAsync(int id)
        {
            var creditCard = await _creditCardRepository.GetByIdAsync(id);
            if (creditCard != null)
            {
                await _creditCardRepository.DeleteAsync(creditCard);
            }
        }
    }
}
