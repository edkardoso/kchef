using System;
using System.Threading.Tasks;
using edk.Kchef.Domain.Common.Base;

namespace edk.Kchef.Domain.Contracts.Repositories;

public interface IDeleteGenericRepository<in TEntity> where TEntity : IEntity
{
    Task DeleteByIdAsync(Guid id);
    Task DeleteAsync(TEntity entity);

}
