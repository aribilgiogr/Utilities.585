using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using Utilities._585.Data.Repositories;
using Utilities._585.Models;

namespace Utilities._585.Data.UnitOfWorks
{
    public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly ConcurrentDictionary<Type, object> repositories;
        public UnitOfWork(TContext context)
        {
            _context = context;
            repositories = [];
        }

        public async Task<Reply> CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                return Reply.Success();
            }
            catch (Exception ex)
            {
                return Reply.Fail(ex.Message);
            }
        }

        public async ValueTask DisposeAsync() => await _context.DisposeAsync();

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class => (IRepository<TEntity>)repositories.GetOrAdd(typeof(TEntity), _ => new Repository<TEntity>(_context));
    }
}
