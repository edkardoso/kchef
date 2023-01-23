using edk.Fusc.Core.Validators;

namespace edk.Fusc.Contracts;

public interface IUseCaseServices
{
    IUseCaseServices AddScoped(Type type);
    IUseCaseServices AddScoped<TService, TValidator, TInput, TOutput>()
        where TService : IUseCase<TInput, TOutput>
        where TValidator : IUseCaseValidator<TInput>;
    IUseCaseServices AddScoped<TService, TValidator, TPresenter>()
        where TService : IUseCase
        where TValidator : IUseCaseValidator
        where TPresenter : IPresenter;
    IUseCaseServices AddScoped<TService>() where TService : IUseCase;
    IServiceProvider BuildServiceProvider();
}