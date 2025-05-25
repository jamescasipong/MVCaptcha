using System.Linq.Expressions;

namespace MVCaptcha.Models.Repositories.RepositoryBase
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAsync(
        Expression<Func<T, bool>> predicate,
        int? take = null);
        Task<List<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
