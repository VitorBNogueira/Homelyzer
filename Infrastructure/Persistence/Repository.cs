using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly DbContext _dbContext;

	public Repository(DbContext context)
	{
        _dbContext = context;
    }
    public async Task<TEntity> GetByIdAsync(int id)
    {
        return await _dbContext.Set<TEntity>().FindAsync(id);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbContext.Set<TEntity>().ToListAsync();
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dbContext.Set<TEntity>().AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await _dbContext.Set<TEntity>().AddRangeAsync(entities);
    }

    public async Task<IEnumerable<TEntity>> FindAsync(Func<TEntity, bool> predicate)
    {
        return _dbContext.Set<TEntity>().Where(predicate);
    }

    public async Task<EntityEntry<TEntity>> RemoveAsync(TEntity entity)
    {
        var result = _dbContext.Set<TEntity>().Remove(entity);
        return result;
    }

    public async Task RemoveRangeAsync(IEnumerable<TEntity> entities)
    {
        _dbContext.Set<TEntity>().RemoveRange(entities);
    }

    public async Task<bool> RemoveByIdAsync(int Id)
    {
        var ad = await GetByIdAsync(Id);
        if (ad is null)
            return false;

        var result= await RemoveAsync(ad);

        if (result.State == EntityState.Deleted)
            return true;

        return false;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }
}
