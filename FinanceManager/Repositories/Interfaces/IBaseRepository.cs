namespace FinanceManager.Repositories.Interfaces
{
    /// <summary>
    /// Interface genérica para repositórios
    /// </summary>
    /// <typeparam name="T">Tipo da entidade</typeparam>
    public interface IBaseRepository<T> where T : class
    {
        /// <summary>
        /// Obter todos os registros
        /// </summary>
        Task<IEnumerable<T>> GetAllAsync();
        
        /// <summary>
        /// Obter registro por ID
        /// </summary>
        Task<T> GetByIdAsync(int id);
        
        /// <summary>
        /// Adicionar um novo registro
        /// </summary>
        Task<T> AddAsync(T entity);
        
        /// <summary>
        /// Atualizar um registro existente
        /// </summary>
        Task<T> UpdateAsync(T entity);
        
        /// <summary>
        /// Remover um registro
        /// </summary>
        Task DeleteAsync(int id);
    }
}
