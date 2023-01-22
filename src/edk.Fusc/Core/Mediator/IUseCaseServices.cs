using edk.Fusc.Core.Presenters;
using edk.Fusc.Core.Validators;

namespace edk.Fusc.Core.Mediator
{
    public interface IUseCaseServices
    {
        UseCaseServices AddScoped(Type type);
        UseCaseServices AddScoped<TService, TValidator, TInput, TOutput>()
            where TService : IUseCase<TInput, TOutput>
            where TValidator : IUseCaseValidator<TInput>;
        UseCaseServices AddScoped<TService, TValidator, TPresenter>()
            where TService : IUseCase
            where TValidator : IUseCaseValidator
            where TPresenter : IPresenter;
        UseCaseServices AddScoped<TService>() where TService : IUseCase;
        IServiceProvider BuildServiceProvider();
    }
}