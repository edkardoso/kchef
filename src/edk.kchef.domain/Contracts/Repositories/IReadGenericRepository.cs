using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using edk.Kchef.Domain.Common;
using edk.Kchef.Domain.Common.Base;

namespace edk.Kchef.Domain.Contracts.Repositories;

public interface IReadGenericRepository<TEntity> where TEntity : IEntity
{
    Task<Option<TEntity>> GetByIdAsync(Guid id);
    Task<Option<TEntity>> FirstOrDefaultAsync();
    Task<Option<TEntity>> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter);
    Task<Option<List<TEntity>>> SearchAsync(Expression<Func<TEntity, bool>> filter);
}
