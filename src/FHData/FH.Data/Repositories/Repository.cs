using FH.Business.Interfaces;
using FH.Business.Models;
using FH.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace FH.Data.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly DataDbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(DataDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<TEntity> GetById(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task Add(TEntity entity)
        {
            _dbSet.Add(entity);
            await SaveChanges();
        }

        public virtual async Task Update(TEntity entity)
        {
            _dbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task Remove(Guid id)
        {
            var entity = new TEntity { Id = id };
            _dbSet.Remove(entity);
            //var entity = await _dbSet.FindAsync(id);
            //_dbSet.Remove(entity);
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}
