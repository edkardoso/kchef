using edk.Fusc.Core.Validators;
namespace edk.Fusc.Contracts;
public interface IUseCaseServices
{
    IUseCaseServices AddScopedWithValidator<TUseCase, TValidator>()
     where TUseCase : IUseCase
     where TValidator : IUseCaseValidator;

    IUseCaseServices AddScopedAll<TUseCase, TValidator, TPresenter>()

        where TUseCase : IUseCase
        where TValidator : IUseCaseValidator
        where TPresenter : IPresenter;
    IUseCaseServices AddScopedUseCase<TUseCase>() where TUseCase : IUseCase;

    IServiceProvider BuildServiceProvider();
}