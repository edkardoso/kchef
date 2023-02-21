using Microsoft.Extensions.DependencyInjection;
using edk.Fusc.Core.Mediator;
using edk.Kchef.Application.Features.GetProducts;
using edk.Kchef.Application.Features.Users.Create;

namespace edk.Kchef.IoC.Extensions;

public static class UseCaseServices
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddMediatorUseCase((mediator) =>
        {
            mediator.Services.AddScoped<CreateUserUseCase, CreateUseCaseValidator, CreateUserInput, UserOutput>();
            mediator.Services.AddScoped<GetProductsUseCase, GetProductsValidator, GetProductsPresenter>();
            mediator.Builder();

        });

        return services;
    }


}
