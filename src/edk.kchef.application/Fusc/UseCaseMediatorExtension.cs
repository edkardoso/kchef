using System;
using Microsoft.Extensions.DependencyInjection;

namespace edk.Kchef.Application.Fusc;

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
