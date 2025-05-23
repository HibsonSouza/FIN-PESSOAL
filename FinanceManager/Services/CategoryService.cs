using FinanceManager.Models;
using FinanceManager.Models.Enums;
using FinanceManager.Repositories.Interfaces;

namespace FinanceManager.Services.Interfaces
{
    /// <summary>
    /// Interface para o serviço de categorias
    /// </summary>
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync(int userId);
        Task<IEnumerable<Category>> GetCategoriesByTypeAsync(TransactionType type, int userId);
        Task<Category?> GetCategoryByIdAsync(int id);
        Task<Category> CreateCategoryAsync(Category category);
        Task<Category> UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(int id);
    }
}

namespace FinanceManager.Services
{
    /// <summary>
    /// Implementação do serviço de categorias
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync(int userId)
        {
            return await _categoryRepository.GetByUserIdAsync(userId);
        }

        public async Task<IEnumerable<Category>> GetCategoriesByTypeAsync(TransactionType type, int userId)
        {
            var allCategories = await _categoryRepository.GetByUserIdAsync(userId);
            return allCategories.Where(c => c.Type == type);
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            return await _categoryRepository.CreateAsync(category);
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            return await _categoryRepository.UpdateAsync(category);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category != null)
            {
                await _categoryRepository.DeleteAsync(category);
            }
        }
    }
}
