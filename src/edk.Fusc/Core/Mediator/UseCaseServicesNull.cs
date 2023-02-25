using edk.Fusc.Contracts;
using edk.Fusc.Core.Validators;

namespace edk.Fusc.Core.Mediator;

public class UseCaseServicesNull : IUseCaseServices
{
    internal UseCaseServicesNull()
    {

    }

    public IUseCaseServices AddScoped(Type type)
    {
        throw new NotImplementedException();
    }

    public IUseCaseServices AddScoped<TService, TValidator, TInput, TOutput>()
        where TService : IUseCase<TInput, TOutput>
        where TValidator : IUseCaseValidator<TInput>
    {
        throw new NotImplementedException();
    }

    public IUseCaseServices AddScoped<TService, TValidator, TPresenter>()
        where TService : IUseCase
        where TValidator : IUseCaseValidator
        where TPresenter : IPresenter
    {
        throw new NotImplementedException();
    }

    public IUseCaseServices AddScoped<TService>() where TService : IUseCase
    {
        throw new NotImplementedException();
    }

    public IUseCaseServices AddScoped<TService, TValidator>()
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
