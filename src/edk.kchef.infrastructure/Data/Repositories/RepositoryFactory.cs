using Microsoft.Extensions.DependencyInjection;
using System;

namespace edk.Kchef.Infrastructure.Data.Repositories
{
    public class RepositoryFactory
    {
        private readonly IServiceProvider _provider;

        public RepositoryFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        public virtual object Get<T>()
          => _provider.GetRequiredService(typeof(T));

        public virtual object Get(Type type)
           => _provider.GetRequiredService(type);
    }
}
