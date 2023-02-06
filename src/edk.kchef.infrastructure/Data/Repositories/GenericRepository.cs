using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using edk.Kchef.Domain.Common.Base;
using edk.Kchef.Infrastructure.Data.EF.Context;
using Microsoft.EntityFrameworkCore;
using edk.Kchef.Domain.Contracts.Repositories;
using edk.Tools;

namespace edk.Kchef.Infrastructure.Data.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity, IAggregateRoot
{
    private readonly KChefContext _dbContext;

    protected DbSet<T> DbSet { get; private set; }
    protected IQueryable<T> Query { get; private set; }

    public GenericRepository(KChefContext dbContext)
    {
        _dbContext = dbContext;
        DbSet = dbContext.Set<T>();
        Query = DbSet.Where(e => !e.Deleted);
    }
    public async Task AddAsync(T entity)
        => _ = await DbSet.AddAsync(entity).ConfigureAwait(false);

    public async Task AddRangeAsync(IReadOnlyCollection<T> entities)
        => await DbSet.AddRangeAsync(entities).ConfigureAwait(false);

    public async Task DeleteAsync(T entity)
    {
        DbSet.Remove(entity);
        await Task.CompletedTask.ConfigureAwait(false);
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var entityOption = await GetByIdAsync(id);

        entityOption.IsNull.Not().WhenTrue(async () => {
            await DeleteAsync(entityOption.Match(e => e, () => null));
        });
     
    }

    public async Task<Option<T>> FirstOrDefaultAsync()
       => new Option<T>(await Query.FirstOrDefaultAsync().ConfigureAwait(false));

    public async Task<Option<T>> FirstOrDefaultAsync(Expression<Func<T, bool>> filter)
       => new Option<T>(await Query.FirstOrDefaultAsync(filter).ConfigureAwait(false));

    public async Task<Option<T>> GetByIdAsync(Guid id)
       => new Option<T>(await Query.FirstOrDefaultAsync(e => e.Id.Equals(id)));

    public Task<Option<List<T>>> SearchAsync(Expression<Func<T, bool>> filter)
       => Task.Run(() => new Option<List<T>>(Query.AsNoTracking().Where(filter).ToList()));

    public async Task UpdateAsync(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await Task.CompletedTask.ConfigureAwait(false);
    }

    public async Task<Option<T>> SingleAsync(Expression<Func<T, bool>> filter) 
        => new Option<T>(await Query.SingleAsync(filter));
}
