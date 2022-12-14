using System;
using Microsoft.Extensions.DependencyInjection;

namespace edk.Kchef.Application.Fusc;

public static class UseCaseMediatorExtension
{
    public static IServiceCollection CreateMediatorUseCase(this IServiceCollection services, Action<UseCaseMediator> mediator)
    {
        mediator.Invoke(new UseCaseMediator(services));
        return services;
    }

    public static string GetNameTranslate(Type sender, Type receiver, dynamic obj)
        => $"{sender.FullName}_{receiver.FullName}={((object)obj).GetType().FullName}";
}
