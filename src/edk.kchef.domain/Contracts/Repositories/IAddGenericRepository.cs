using System.Collections.Generic;
using System.Threading.Tasks;
using edk.Kchef.Domain.Common.Base;

namespace edk.Kchef.Domain.Contracts.Repositories;

public interface IAddGenericRepository<in TEntity> where TEntity : IEntity
{
    Task AddAsync(TEntity entity);
    Task AddRangeAsync(IReadOnlyCollection<TEntity> entities);
}
