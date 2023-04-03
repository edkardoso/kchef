using edk.Fusc.Contracts;
using edk.Fusc.Core.Validators;
using Microsoft.Extensions.DependencyInjection;
using System.Collections;

namespace edk.Fusc.Core.Mediator;
public class UseCaseServices : IUseCaseServices
{
    private readonly IServiceCollection _services;
    private readonly Hashtable _validators; // a opção pelo Hashtable sobre o Dictionary se deu por o mesmo sempre ser Thread-Safe : https://www.macoratti.net/21/03/c_hashdict1.htm
    private readonly Hashtable _presenters;
    internal UseCaseServices(IServiceCollection services)
    {
        _services = services;
        _validators = new Hashtable();
        _presenters = new Hashtable();
    }

    internal Hashtable ValidatorsTable => _validators;

    internal Hashtable PresentersTable => _presenters;

    public IUseCaseServices AddScoped<TUseCase>() where TUseCase : IUseCase
      => AddScoped(typeof(TUseCase));

    public IUseCaseServices AddScoped<TUseCase, TValidator>()
       where TUseCase : IUseCase
       where TValidator : IUseCaseValidator
    {
        ValidatorsTable.Add(typeof(TUseCase).Name, typeof(TValidator));

        return AddScoped(typeof(TUseCase))
                .AddScoped(typeof(TValidator));
    }

    public IUseCaseServices AddScoped<TUseCase, TValidator, TPresenter>()
       where TUseCase : IUseCase
       where TValidator : IUseCaseValidator
       where TPresenter : IPresenter
    {

        ValidatorsTable.Add(typeof(TUseCase).Name, typeof(TValidator));
        PresentersTable.Add(typeof(TUseCase).Name, typeof(TPresenter));

        return AddScoped(typeof(TUseCase))
            .AddScoped(typeof(TValidator))
            .AddScoped(typeof(TPresenter));
    }

    public IUseCaseServices AddScoped(Type type)
    {
        _services.Add(new ServiceDescriptor(
            type,
            serviceProvider =>
            {
               var obj = ActivatorUtilities.CreateInstance(serviceProvider, type);
                return obj;
            }, ServiceLifetime.Scoped));

       // _services.AddScoped(type);
        return this;
    }



    public IServiceProvider BuildServiceProvider() => _services.BuildServiceProvider();
}
