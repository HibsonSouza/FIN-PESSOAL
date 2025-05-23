using FinanceManager.Models;
using FinanceManager.Models.Enums;
using FinanceManager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinanceManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IAccountService _accountService;

        public TransactionsController(
            ITransactionService transactionService,
            IAccountService accountService)
        {
            _transactionService = transactionService;
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] TransactionFilterDto filter)
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                return Unauthorized();
            }

            var transactions = await _transactionService.GetFilteredTransactionsAsync(
                userId, 
                filter.StartDate, 
                filter.EndDate, 
                filter.AccountId, 
                filter.CategoryId, 
                filter.Type, 
                filter.SearchTerm);

            return Ok(transactions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                return Unauthorized();
            }

            var transaction = await _transactionService.GetTransactionByIdAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            // Verificar se a transação pertence a uma conta do usuário
            var account = await _accountService.GetAccountByIdAsync(transaction.AccountId);
            if (account == null || account.UserId != userId)
            {
                return NotFound();
            }

            return Ok(transaction);
        }

        [HttpGet("account/{accountId}")]
        public async Task<IActionResult> GetByAccountId(int accountId)
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                return Unauthorized();
            }

            // Verificar se a conta pertence ao usuário
            var account = await _accountService.GetAccountByIdAsync(accountId);
            if (account == null || account.UserId != userId)
            {
                return NotFound();
            }

            var transactions = await _transactionService.GetTransactionsByAccountAsync(accountId);
            return Ok(transactions);
        }

        [HttpGet("recent")]
        public async Task<IActionResult> GetRecent([FromQuery] int count = 5)
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                return Unauthorized();
            }

            var transactions = await _transactionService.GetRecentTransactionsAsync(userId, count);
            return Ok(transactions);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                return Unauthorized();
            }

            // Verificar se a conta pertence ao usuário
            var account = await _accountService.GetAccountByIdAsync(transaction.AccountId);
            if (account == null || account.UserId != userId)
            {
                return BadRequest("Conta inválida");
            }

            var result = await _transactionService.CreateTransactionAsync(transaction);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Transaction transaction)
        {
            if (id != transaction.Id)
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

            // Verificar se a transação existe e pertence ao usuário
            var existingTransaction = await _transactionService.GetTransactionByIdAsync(id);
            if (existingTransaction == null)
            {
                return NotFound();
            }

            var existingAccount = await _accountService.GetAccountByIdAsync(existingTransaction.AccountId);
            if (existingAccount == null || existingAccount.UserId != userId)
            {
                return NotFound();
            }

            // Verificar se a nova conta pertence ao usuário
            var newAccount = await _accountService.GetAccountByIdAsync(transaction.AccountId);
            if (newAccount == null || newAccount.UserId != userId)
            {
                return BadRequest("Conta inválida");
            }

            var result = await _transactionService.UpdateTransactionAsync(transaction);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                return Unauthorized();
            }

            // Verificar se a transação existe e pertence ao usuário
            var existingTransaction = await _transactionService.GetTransactionByIdAsync(id);
            if (existingTransaction == null)
            {
                return NotFound();
            }

            var existingAccount = await _accountService.GetAccountByIdAsync(existingTransaction.AccountId);
            if (existingAccount == null || existingAccount.UserId != userId)
            {
                return NotFound();
            }

            await _transactionService.DeleteTransactionAsync(id);

            return NoContent();
        }
    }

    public class TransactionFilterDto
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? AccountId { get; set; }
        public int? CategoryId { get; set; }
        public TransactionType? Type { get; set; }
        public string? SearchTerm { get; set; }
    }
}
