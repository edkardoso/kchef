using edk.Kchef.Domain.Contracts.Services;
using edk.Kchef.Domain.Entities.Users;
using edk.Kchef.Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace edk.Kchef.IoC.Extensions;

public static class DomainServicesServices
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        services.AddScoped<IPasswordService, PasswordService>();
   
        return services;
    }
}
