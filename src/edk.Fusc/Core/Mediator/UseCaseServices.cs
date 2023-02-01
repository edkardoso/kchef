using edk.Fusc.Contracts;
using edk.Fusc.Core.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace edk.Fusc.Core.Mediator;
public class UseCaseServices : IUseCaseServices
{
    private readonly IServiceCollection _services;

    internal UseCaseServices(IServiceCollection services)
    {
        _services = services;
    }

    public IUseCaseServices AddScoped<TService>() where TService : IUseCase
      => AddScoped(typeof(TService));

    public IUseCaseServices AddScoped<TService, TValidator, TInput, TOutput>()
        where TService : IUseCase<TInput, TOutput>
        where TValidator : IUseCaseValidator<TInput>
        => AddScoped(typeof(TService))
            .AddScoped(typeof(TValidator));

    public IUseCaseServices AddScoped<TService, TValidator, TPresenter>()
       where TService : IUseCase
       where TValidator : IUseCaseValidator
       where TPresenter : IPresenter
        => AddScoped(typeof(TService))
        .AddScoped(typeof(TValidator))
        .AddScoped(typeof(TPresenter));


    public IUseCaseServices AddScoped(Type type)
    {
        _services.AddScoped(type);
        return this;
    }

    public IServiceProvider BuildServiceProvider() => _services.BuildServiceProvider();
}
