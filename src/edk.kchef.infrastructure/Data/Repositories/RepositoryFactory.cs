using edk.Kchef.Domain.Contracts.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace edk.Kchef.Infrastructure.Data.Repositories
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly IServiceProvider _provider;

        public RepositoryFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        public virtual object Get<T>() where T: IGenericRepository
          => _provider.GetRequiredService(typeof(T));

        public virtual object Get(Type type)
           =>  _provider.GetRequiredService(type);
    }
}
