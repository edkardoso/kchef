using edk.Fusc.Contracts;
using edk.Fusc.Core.Validators;
using Microsoft.Extensions.DependencyInjection;
using System.Collections;

namespace edk.Fusc.Core.Mediator;
public class UseCaseServicesExtension : IUseCaseServices
{
    private readonly IServiceCollection _services;
    private readonly Hashtable _validators; // a opção pelo Hashtable sobre o Dictionary se deu por o mesmo sempre ser Thread-Safe : https://www.macoratti.net/21/03/c_hashdict1.htm
    private readonly Hashtable _presenters;
    internal UseCaseServicesExtension(IServiceCollection services)
    {
        _services = services;
        _validators = new Hashtable(); // TODO: Verificar o uso do Hashtable, pq acredito que o Dicionary sej mais performático
        _presenters = new Hashtable();
    }

    internal Hashtable ValidatorsTable => _validators;

    internal Hashtable PresentersTable => _presenters;

    public IUseCaseServices AddScopedUseCase<TUseCase>() where TUseCase : IUseCase
      => AddLifeTime(typeof(TUseCase));

    public IUseCaseServices AddScopedWithValidator<TUseCase, TValidator>()
       where TUseCase : IUseCase
       where TValidator : IUseCaseValidator
    {
        ValidatorsTable.Add(typeof(TUseCase).Name, typeof(TValidator));

        return AddLifeTime(typeof(TUseCase))
                .AddLifeTime(typeof(TValidator));
    }

    public IUseCaseServices AddScopedWithPresenter<TUseCase, TPresenter>()
      where TUseCase : IUseCase
      where TPresenter : IPresenter
    {
        PresentersTable.Add(typeof(TUseCase).Name, typeof(TPresenter));

        return AddLifeTime(typeof(TUseCase))
                .AddLifeTime(typeof(TPresenter));
    }

    public IUseCaseServices AddScopedAll<TUseCase, TValidator, TPresenter>()
       where TUseCase : IUseCase
       where TValidator : IUseCaseValidator
       where TPresenter : IPresenter
    {

        ValidatorsTable.Add(typeof(TUseCase).Name, typeof(TValidator));
        PresentersTable.Add(typeof(TUseCase).Name, typeof(TPresenter));

        return AddLifeTime(typeof(TUseCase))
            .AddLifeTime(typeof(TValidator))
            .AddLifeTime(typeof(TPresenter));
    }

    private UseCaseServicesExtension AddLifeTime(Type type, ServiceLifetime lifetime = ServiceLifetime.Scoped)
    {
        _services.Add(new ServiceDescriptor(
            type,
            serviceProvider =>
            {
                var obj = ActivatorUtilities.CreateInstance(serviceProvider, type);
                return obj;
            }, lifetime));

        // _services.AddScoped(type);
        return this;
    }

    public IServiceProvider BuildServiceProvider() => _services.BuildServiceProvider();

  
}
