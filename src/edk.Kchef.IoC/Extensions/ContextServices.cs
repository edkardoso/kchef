using edk.Kchef.Infrastructure.Data.EF.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace edk.Kchef.IoC.Extensions;

public static class ContextServices
{
    public static IServiceCollection AddContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<KChefContext>(options =>
        {
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });

        return services;
    }
}

public static class MediatRServices
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        return services;
    }

    
}
