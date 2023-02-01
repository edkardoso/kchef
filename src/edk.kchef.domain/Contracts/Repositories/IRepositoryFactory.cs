using System;

namespace edk.Kchef.Domain.Contracts.Repositories;

public interface IRepositoryFactory
{
    object Get(Type type);
    object Get<T>();
}