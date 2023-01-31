using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using edk.Kchef.Domain.Common.Base;

namespace edk.Kchef.Domain.Contracts.Repositories;

public interface IReadGenericRepository<TEntity> where TEntity : IEntity
{
    Task<TEntity> GetByIdAsync(Guid id);
    Task<TEntity> FirstOrDefaultAsync();
    Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter);
    Task<List<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> filter);
}
