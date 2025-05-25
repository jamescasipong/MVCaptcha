using Microsoft.EntityFrameworkCore;
using MVCaptcha.Data;
using System.Linq.Expressions;

namespace MVCaptcha.Models.Repositories.RepositoryBase
{
    public class RepositoryBase<T>: IRepositoryBase<T> where T : class
    {
        protected readonly AppDataContext _context;
        private readonly DbSet<T> _dbSet;

        public RepositoryBase(AppDataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = context.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id) ?? throw new KeyNotFoundException($"Entity of type {typeof(T).Name} with ID {id} not found.");
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return false;
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> ExistsAsync(int id)
        {
            return await _dbSet.AnyAsync(e => EF.Property<int>(e, "Id") == id);
        }

        public async Task<IEnumerable<T>> GetAsync(
        Expression<Func<T, bool>> predicate,
        int? take = null)
        {
            IQueryable<T> query = _dbSet.Where(predicate);

            if (take.HasValue)
                query = query.Take(take.Value);

            var result = await query.ToListAsync();

            return result.Any()
                ? result
                : throw new KeyNotFoundException($"Entity of type {typeof(T).Name} not found with the specified condition.");
        }

    }
}
