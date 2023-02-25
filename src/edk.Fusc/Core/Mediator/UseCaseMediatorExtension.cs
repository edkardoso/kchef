using edk.Fusc.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace edk.Fusc.Core.Mediator;

public static class UseCaseMediatorExtension
{
    public static IServiceCollection AddFusc(this IServiceCollection services, Action<UseCaseMediator> mediator)
    {
       
        var instance = new UseCaseMediator(services);

        services.AddScoped<IMediatorUseCase>(x =>
        {
            return instance;
        });

        mediator.Invoke(instance);

        return services;
    }

   
}
