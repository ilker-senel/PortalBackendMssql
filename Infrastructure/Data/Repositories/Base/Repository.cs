using Infrastructure.Data.EntityFramework;
using Infrastructure.Data.Repositories.Base.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Infrastructure.Data.Repositories.Base
{
    public abstract class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class
    {
        private readonly AppDbContext dbContext;

        protected Repository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        private DbSet<TEntity> Table { get => dbContext.Set<TEntity>(); }

        public virtual async Task AddAsync(TEntity entity)
        {
            await Table.AddAsync(entity);
        }

        public virtual async Task AddRangeAsync(IList<TEntity> entities)
        {
            await Table.AddRangeAsync(entities);
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null)
        {
            Table.AsNoTracking();
            if (predicate is not null) Table.Where(predicate);
            return await Table.CountAsync();
        }

        public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, bool enableTracking = false)
        {
            if (!enableTracking) Table.AsNoTracking();
            return Table.Where(predicate);
        }

        public virtual async Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, bool enableTracking = false)
        {
            IQueryable<TEntity> queryable = Table;
            if (!enableTracking) queryable = queryable.AsNoTracking();
            if (include is not null) queryable = include(queryable);
            if (predicate is not null) queryable = queryable.Where(predicate);
            if (orderBy is not null)
                return await orderBy(queryable).ToListAsync();

            return await queryable.ToListAsync();
        }

        public virtual async Task<IList<TEntity>> GetAllByPagingAsync(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, bool enableTracking = false, int currentPage = 1, int pageSize = 3)
        {
            IQueryable<TEntity> queryable = Table;
            if (!enableTracking) queryable = queryable.AsNoTracking();
            if (include is not null) queryable = include(queryable);
            if (predicate is not null) queryable = queryable.Where(predicate);
            if (orderBy is not null)
                return await orderBy(queryable).Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();

            return await queryable.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool enableTracking = false)
        {
            IQueryable<TEntity> queryable = Table;
            if (!enableTracking) queryable = queryable.AsNoTracking();
            if (include is not null) queryable = include(queryable);
            //queryable.Where(predicate);


            return await queryable.FirstOrDefaultAsync(predicate);
        }

        public async Task<TEntity> GetByIdAsync(TId id)
        {
            return await Table.FindAsync(id);
        }

        public virtual async Task HardDeleteAsync(TEntity entity)
        {
            await Task.Run(() => Table.Remove(entity));
        }

        public virtual async Task HardDeleteRangeAsync(IList<TEntity> entity)
        {
            await Task.Run(() => Table.RemoveRange(entity));
        }

        public async Task HardDeleteByIdAsync(TId id)
        {
            var entity = await Table.FindAsync(id);
            if (entity != null)
                await Task.Run(() => Table.Remove(entity));

        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            await Task.Run(() => Table.Update(entity));
            return entity;
        }
    }
}
