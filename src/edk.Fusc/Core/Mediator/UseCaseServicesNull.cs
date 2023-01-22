using edk.Fusc.Core.Presenters;
using edk.Fusc.Core.Validators;

namespace edk.Fusc.Core.Mediator;

public class UseCaseServicesNull : IUseCaseServices
{
    public UseCaseServicesNull()
    {

    }

    public UseCaseServices AddScoped(Type type)
    {
        throw new NotImplementedException();
    }

    public UseCaseServices AddScoped<TService, TValidator, TInput, TOutput>()
        where TService : IUseCase<TInput, TOutput>
        where TValidator : IUseCaseValidator<TInput>
    {
        throw new NotImplementedException();
    }

    public UseCaseServices AddScoped<TService, TValidator, TPresenter>()
        where TService : IUseCase
        where TValidator : IUseCaseValidator
        where TPresenter : IPresenter
    {
        throw new NotImplementedException();
    }

    public UseCaseServices AddScoped<TService>() where TService : IUseCase
    {
        throw new NotImplementedException();
    }

    public IServiceProvider BuildServiceProvider()
    {
        throw new NotImplementedException();
    }
}
