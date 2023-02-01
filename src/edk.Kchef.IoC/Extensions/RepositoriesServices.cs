using edk.Kchef.Domain.Contracts.Repositories;
using edk.Kchef.Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace edk.Kchef.IoC.Extensions;

public static class RepositoriesServices
{
    public static IServiceCollection AddRepository(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IRepositoryFactory, RepositoryFactory>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        return services;
    }
}
