using System.Linq.Expressions;

namespace Utilities._585.Data.Repositories
{
    /// <summary>
    /// TEntity türündeki varlıklar için ortak asenkron ve senkron CRUD işlemlerini tanımlar.
    /// </summary>
    public interface IRepository<TEntity> where TEntity : class
    {
        // READ
        /// <summary>
        /// Belirtilen anahtara göre varlığı getirir veya bulunamazsa null döner.
        /// </summary>
        Task<TEntity?> GetByKeyAsync(object key, CancellationToken cancellationToken = default);

        /// <summary>
        /// Tüm varlıkları IQueryable olarak döner; isteğe bağlı olarak değişiklik takibini devre dışı bırakır.
        /// </summary>
        IQueryable<TEntity> GetAll(bool enableTracking = false);

        /// <summary>
        /// Belirtilen koşulu sağlayan en az bir varlık olup olmadığını kontrol eder.
        /// </summary>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? where = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Tüm varlıkların belirtilen koşulu sağlayıp sağlamadığını kontrol eder.
        /// </summary>
        Task<bool> AllAsync(Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken = default);

        /// <summary>
        /// İsteğe bağlı koşula uyan varlıkların sayısını döner.
        /// </summary>
        Task<int> CountAsync(Expression<Func<TEntity, bool>>? where = null, CancellationToken cancellationToken = default);


        // CREATE
        /// <summary>
        /// Yeni bir varlığı veri deposuna asenkron olarak ekler.
        /// </summary>
        Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Birden çok varlığı veri deposuna asenkron olarak ekler.
        /// </summary>
        Task CreateManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        // Update
        /// <summary>
        /// Verilen varlığı değiştirilmiş olarak işaretler, böylece değişiklikler kaydedilebilir.
        /// </summary>
        void Update(TEntity entity);

        /// <summary>
        /// Birden çok varlığı değiştirilmiş olarak işaretler, böylece değişiklikler kaydedilebilir.
        /// </summary>
        void UpdateMany(IEnumerable<TEntity> entities);

        // DELETE
        /// <summary>
        /// Belirtilen varlığı veri deposundan kaldırır.
        /// </summary>
        void Delete(TEntity entity);

        /// <summary>
        /// Verilen varlık koleksiyonunu veri deposundan kaldırır.
        /// </summary>
        void DeleteMany(IEnumerable<TEntity> entities);

        /// <summary>
        /// Verilen koşulu sağlayan varlıkları veri deposundan kaldırır.
        /// </summary>
        void DeleteMany(Expression<Func<TEntity, bool>>? where = null);
    }
}
