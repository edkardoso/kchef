using edk.Kchef.Infrastructure.Data.EF.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace edk.Kchef.IoC.Extensions;

public static class ContextServices
{
    public static IServiceCollection AddContext(this IServiceCollection services, string connectionString)
    {
        return services;
    }
}
