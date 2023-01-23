using System;
using edk.Fusc.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace edk.Fusc.Core.Mediator;

public static class UseCaseMediatorExtension
{
    public static IServiceCollection AddMediatorUseCase(this IServiceCollection services, Action<UseCaseMediator> mediator)
    {
       
        var instance = new UseCaseMediator(services);

        services.AddScoped<IMediatorUseCase>(x =>
        {
            return instance;
        });

        mediator.Invoke(instance);

        return services;
    }

    public static string GetNameTranslate(Type sender, Type receiver, dynamic obj)
        => $"{sender.FullName}_{receiver.FullName}={((object)obj).GetType().FullName}";
}
