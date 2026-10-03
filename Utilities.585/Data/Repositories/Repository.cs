using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Utilities._585.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _set;

        public Repository(DbContext context)
        {
            _context = context;
            _set = _context.Set<TEntity>();
        }

        public async Task<bool> AllAsync(Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken = default) => await _set.AllAsync(where, cancellationToken);

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? where = null, CancellationToken cancellationToken = default) => await _set.AnyAsync(where ?? (x => true), cancellationToken);

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? where = null, CancellationToken cancellationToken = default) => await _set.CountAsync(where ?? (x => true), cancellationToken);

        public async Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default) => await _set.AddAsync(entity, cancellationToken);

        public async Task CreateManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) => await _set.AddRangeAsync(entities, cancellationToken);

        public void Delete(TEntity entity) => _set.Remove(entity);

        public void DeleteMany(IEnumerable<TEntity> entities) => _set.RemoveRange(entities);

        public void DeleteMany(Expression<Func<TEntity, bool>>? where = null)
        {
            var entities = _set.Where(where ?? (x => true));
            _set.RemoveRange(entities);
        }

        public IQueryable<TEntity> GetAll(bool enableTracking = false) => enableTracking ? _set : _set.AsNoTracking();

        public async Task<TEntity?> GetByKeyAsync(object key, CancellationToken cancellationToken = default) => await _set.FindAsync(key, cancellationToken);

        public void Update(TEntity entity) => _set.Update(entity);

        public void UpdateMany(IEnumerable<TEntity> entities) => _set.UpdateRange(entities);
    }
}
