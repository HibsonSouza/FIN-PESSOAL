using FinanceManager.Models;
using FinanceManager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinanceManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CreditCardsController : ControllerBase
    {
        private readonly ICreditCardService _creditCardService;

        public CreditCardsController(ICreditCardService creditCardService)
        {
            _creditCardService = creditCardService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                return Unauthorized();
            }

            var creditCards = await _creditCardService.GetAllCreditCardsAsync(userId);
            return Ok(creditCards);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                return Unauthorized();
            }

            var creditCard = await _creditCardService.GetCreditCardByIdAsync(id);
            if (creditCard == null || creditCard.UserId != userId)
            {
                return NotFound();
            }

            return Ok(creditCard);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreditCard creditCard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                return Unauthorized();
            }

            // Verificar se já existe um cartão com o mesmo número
            var existingCard = await _creditCardService.GetCreditCardByNumberAsync(creditCard.CardNumber);
            if (existingCard != null)
            {
                return BadRequest("Já existe um cartão cadastrado com este número");
            }

            creditCard.UserId = userId;
            var result = await _creditCardService.CreateCreditCardAsync(creditCard);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreditCard creditCard)
        {
            if (id != creditCard.Id)
            {
                return BadRequest("ID na rota não corresponde ao ID no corpo da requisição");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                return Unauthorized();
            }

            var existingCard = await _creditCardService.GetCreditCardByIdAsync(id);
            if (existingCard == null || existingCard.UserId != userId)
            {
                return NotFound();
            }

            // Verificar se o número do cartão foi alterado e se já existe um cartão com este número
            if (existingCard.CardNumber != creditCard.CardNumber)
            {
                var cardWithSameNumber = await _creditCardService.GetCreditCardByNumberAsync(creditCard.CardNumber);
                if (cardWithSameNumber != null)
                {
                    return BadRequest("Já existe um cartão cadastrado com este número");
                }
            }

            creditCard.UserId = userId;
            var result = await _creditCardService.UpdateCreditCardAsync(creditCard);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                return Unauthorized();
            }

            var creditCard = await _creditCardService.GetCreditCardByIdAsync(id);
            if (creditCard == null || creditCard.UserId != userId)
            {
                return NotFound();
            }

            await _creditCardService.DeleteCreditCardAsync(id);

            return NoContent();
        }
    }
}
