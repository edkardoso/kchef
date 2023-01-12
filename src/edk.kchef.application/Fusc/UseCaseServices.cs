using System;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace edk.Kchef.Application.Fusc;

public class UseCaseServices
{
    private readonly IServiceCollection _services;

    public UseCaseServices(IServiceCollection services)
    {
        _services = services;
    }

    public UseCaseServices AddScoped<TService>() where TService : IUseCase
      => AddScoped(typeof(TService));

    public UseCaseServices AddScoped<TService, TValidator, TInput, TOutput>()
        where TService : IUseCase<TInput, TOutput>
        where TValidator : IValidator<TInput>
        => AddScoped(typeof(TService))
            .AddScoped(typeof(TValidator));

    public UseCaseServices AddScoped<TService, TValidator, TPresenter>()
       where TService : IUseCase
       where TValidator : IValidator
       where TPresenter : IPresenter
        => AddScoped(typeof(TService))
        .AddScoped(typeof(TValidator))
        .AddScoped(typeof(TPresenter));


    public UseCaseServices AddScoped(Type type)
    {
        _services.AddScoped(type);
        return this;
    }

    public IServiceProvider BuildServiceProvider() => _services.BuildServiceProvider();
}
