using edk.Fusc.Core.Validators;

namespace edk.Fusc.Contracts;

public interface IUseCaseServices
{
    IUseCaseServices AddScoped(Type type);

    IUseCaseServices AddScoped<TUseCase, TValidator>()
     where TUseCase : IUseCase
     where TValidator : IUseCaseValidator;

    IUseCaseServices AddScoped<TUseCase, TValidator, TPresenter>()
        where TUseCase : IUseCase
        where TValidator : IUseCaseValidator
        where TPresenter : IPresenter;
    IUseCaseServices AddScoped<TUseCase>() where TUseCase : IUseCase;
    IServiceProvider BuildServiceProvider();
}