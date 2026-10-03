using Utilities._585.Data.Repositories;
using Utilities._585.Models;

namespace Utilities._585.Data.UnitOfWorks
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        Task<Reply> CommitAsync();
    }
}
