using edk.Kchef.Domain.Common.Base;

namespace edk.Kchef.Domain.Contracts.Repositories;

public interface IGenericRepository { }

public interface IGenericRepository<TEntity> : IGenericRepository
    , IReadGenericRepository<TEntity> 
    , IEditGenericRepository<TEntity>
    , IDeleteGenericRepository<TEntity>
    where TEntity : IEntity
{ }
