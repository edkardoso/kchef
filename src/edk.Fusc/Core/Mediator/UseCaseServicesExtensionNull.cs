using edk.Fusc.Contracts;
using edk.Fusc.Core.Validators;

namespace edk.Fusc.Core.Mediator;

public class UseCaseServicesExtensionNull : IUseCaseServices
{
    internal UseCaseServicesExtensionNull()
    {

    }

    public IUseCaseServices AddLifeTime(Type type)
    {
        throw new NotImplementedException();
    }

    public IUseCaseServices AddScoped<TService, TValidator, TInput, TOutput>()
        where TService : IUseCase<TInput, TOutput>
        where TValidator : IUseCaseValidator<TInput>
    {
        throw new NotImplementedException();
    }

    public IUseCaseServices AddScopedAll<TService, TValidator, TPresenter>()
        where TService : IUseCase
        where TValidator : IUseCaseValidator
        where TPresenter : IPresenter
    {
        throw new NotImplementedException();
    }

    public IUseCaseServices AddScopedUseCase<TService>() where TService : IUseCase
    {
        throw new NotImplementedException();
    }

    public IUseCaseServices AddScopedWithValidator<TService, TValidator>()
        where TService : IUseCase
        where TValidator : IUseCaseValidator
    {
        throw new NotImplementedException();
    }

    public IServiceProvider BuildServiceProvider()
    {
        throw new NotImplementedException();
    }
}
