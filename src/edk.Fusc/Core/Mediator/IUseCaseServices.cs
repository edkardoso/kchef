using edk.Fusc.Core.Presenters;
using FluentValidation;

namespace edk.Fusc.Core.Mediator
{
    public interface IUseCaseServices
    {
        UseCaseServices AddScoped(Type type);
        UseCaseServices AddScoped<TService, TValidator, TInput, TOutput>()
            where TService : IUseCase<TInput, TOutput>
            where TValidator : IValidator<TInput>;
        UseCaseServices AddScoped<TService, TValidator, TPresenter>()
            where TService : IUseCase
            where TValidator : IValidator
            where TPresenter : IPresenter;
        UseCaseServices AddScoped<TService>() where TService : IUseCase;
        IServiceProvider BuildServiceProvider();
    }
}