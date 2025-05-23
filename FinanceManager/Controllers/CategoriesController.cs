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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                return Unauthorized();
            }

            var categories = await _categoryService.GetAllCategoriesAsync(userId);
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            // Verificar se a categoria pertence ao usuário ou é uma categoria padrão (UserId == null)
            if (category.UserId.HasValue)
            {
                if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId) || category.UserId != userId)
                {
                    return NotFound();
                }
            }

            return Ok(category);
        }

        [HttpGet("type/{type}")]
        public async Task<IActionResult> GetByType(TransactionType type)
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                return Unauthorized();
            }

            var categories = await _categoryService.GetCategoriesByTypeAsync(type, userId);
            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                return Unauthorized();
            }

            category.UserId = userId;
            var result = await _categoryService.CreateCategoryAsync(category);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Category category)
        {
            if (id != category.Id)
            {
                return BadRequest("ID na rota não corresponde ao ID no corpo da requisição");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingCategory = await _categoryService.GetCategoryByIdAsync(id);
            if (existingCategory == null)
            {
                return NotFound();
            }

            // Verificar se a categoria pertence ao usuário
            if (existingCategory.UserId.HasValue)
            {
                if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId) || existingCategory.UserId != userId)
                {
                    return NotFound();
                }

                category.UserId = userId;
            }
            else
            {
                // Não permitir edição de categorias padrão
                return BadRequest("Categorias padrão não podem ser editadas");
            }

            var result = await _categoryService.UpdateCategoryAsync(category);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            // Verificar se a categoria pertence ao usuário
            if (category.UserId.HasValue)
            {
                if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId) || category.UserId != userId)
                {
                    return NotFound();
                }
            }
            else
            {
                // Não permitir exclusão de categorias padrão
                return BadRequest("Categorias padrão não podem ser excluídas");
            }

            await _categoryService.DeleteCategoryAsync(id);

            return NoContent();
        }
    }
}
