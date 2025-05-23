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
    public class GoalsController : ControllerBase
    {
        private readonly IGoalService _goalService;

        public GoalsController(IGoalService goalService)
        {
            _goalService = goalService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                return Unauthorized();
            }

            var goals = await _goalService.GetAllGoalsAsync(userId);
            return Ok(goals);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                return Unauthorized();
            }

            var goal = await _goalService.GetGoalByIdAsync(id);
            if (goal == null || goal.UserId != userId)
            {
                return NotFound();
            }

            return Ok(goal);
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActive()
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                return Unauthorized();
            }

            var goals = await _goalService.GetActiveGoalsAsync(userId);
            return Ok(goals);
        }

        [HttpGet("completed")]
        public async Task<IActionResult> GetCompleted()
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                return Unauthorized();
            }

            var goals = await _goalService.GetCompletedGoalsAsync(userId);
            return Ok(goals);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Goal goal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                return Unauthorized();
            }

            goal.UserId = userId;
            goal.CurrentAmount = 0; // Inicializa com valor zero
            
            var result = await _goalService.CreateGoalAsync(goal);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Goal goal)
        {
            if (id != goal.Id)
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

            var existingGoal = await _goalService.GetGoalByIdAsync(id);
            if (existingGoal == null || existingGoal.UserId != userId)
            {
                return NotFound();
            }

            goal.UserId = userId;
            var result = await _goalService.UpdateGoalAsync(goal);

            return Ok(result);
        }

        [HttpPost("{id}/contribute")]
        public async Task<IActionResult> AddContribution(int id, [FromBody] ContributionDto contribution)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                return Unauthorized();
            }

            var existingGoal = await _goalService.GetGoalByIdAsync(id);
            if (existingGoal == null || existingGoal.UserId != userId)
            {
                return NotFound();
            }

            if (contribution.Amount <= 0)
            {
                return BadRequest("O valor da contribuição deve ser maior que zero");
            }

            var result = await _goalService.AddContributionAsync(id, contribution.Amount);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                return Unauthorized();
            }

            var goal = await _goalService.GetGoalByIdAsync(id);
            if (goal == null || goal.UserId != userId)
            {
                return NotFound();
            }

            await _goalService.DeleteGoalAsync(id);

            return NoContent();
        }
    }

    public class ContributionDto
    {
        public decimal Amount { get; set; }
    }
}
