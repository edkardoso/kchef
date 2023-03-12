using Microsoft.Extensions.DependencyInjection;
using edk.Fusc.Core.Mediator;
using edk.Kchef.Application.Features.GetProducts;
using edk.Kchef.Application.Features.Users.Create;

namespace edk.Kchef.IoC.Extensions;

public static class UseCaseServices
{
    public static IServiceCollection AddFusc(this IServiceCollection services)
    {
        services.AddFusc((mediator) =>
        {
            mediator.Services.AddScoped<CreateUserUseCase, CreateUseCaseValidator, CreateUserPresenter>();
            mediator.Services.AddScoped<GetProductsUseCase, GetProductsValidator, GetProductsPresenter>();
            mediator.Builder();

        });

        return services;
    }


}
