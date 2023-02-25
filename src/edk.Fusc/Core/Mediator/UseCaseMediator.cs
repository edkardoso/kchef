using edk.Fusc.Contracts;
using edk.Fusc.Core.Events;
using edk.Fusc.Core.Presenters;
using edk.Fusc.Core.Validators;
using edk.Tools;
using Microsoft.Extensions.DependencyInjection;

namespace edk.Fusc.Core.Mediator;
public class UseCaseMediator : IMediatorUseCase
{
    private readonly Dictionary<string, Type> _translateDictionary = new();
    public virtual IFactoryMediator Factory { get; private set; }
    public IUseCaseServices Services { get; private set; }
    public IUser User { get; private set; }
    internal ObserverMediator Observer { get; private set; } = new();

    public bool IsProduction { get; private set; }
    public bool IsDevelopment => IsProduction.Not();

    public UseCaseMediator() : this(new UseCaseServicesNull(), new FactoryMediatorNull())
    { }

    public UseCaseMediator(IFactoryMediator factory) : this(new UseCaseServicesNull(), factory)
    { }

    public UseCaseMediator(IUseCaseServices services, IFactoryMediator factory)
    {
        Services = services;
        Factory = factory;
        User = new UserNull();
    }

    public UseCaseMediator(IServiceCollection services)
    {
        Factory = new FactoryMediatorNull();
        Services = new UseCaseServices(services);
        User = new UserNull();
    }

    public void Builder()
        => Factory = new FactoryMediator(Services.BuildServiceProvider());

    /// <summary>
    /// Obtém uma instância do UseCase e o executa
    /// </summary>
    public async Task<IPresenter> HandleAsync<TUseCase>(dynamic input)
       where TUseCase : IUseCase
    {
        var useCase = (TUseCase)Factory.Get<TUseCase>();

        if (useCase.HasMediator.IsFalse())
            useCase.SetMediator(this);

        if (useCase.HasValidator.IsFalse())
            SetValidatorInUseCase(useCase);

        if (useCase.HasPresenter.IsFalse())
            SetPresenterInUseCase(useCase);

        return await useCase.HandleAsync(input);
    }

    private void SetValidatorInUseCase<TUseCase>(TUseCase useCase) where TUseCase : IUseCase
    {

        var validatorType = GetTypeOfValidatorTable(typeof(TUseCase).Name);

        if (validatorType != null)
        {
            var validator = (IUseCaseValidator)Factory.Get(validatorType);
            if (validator != null)
            {
                useCase.SetValidator(validator);

            }
        }
    }

    private void SetPresenterInUseCase<TUseCase>(TUseCase useCase) where TUseCase : IUseCase
    {


        var presenterType = GetTypeOfPresenterTable(typeof(TUseCase).Name);

        if (presenterType != null)
        {
            var presenter = (IPresenter)Factory.Get(presenterType);
            if (presenter != null)
            {
                useCase.SetPresenter(presenter);

            }
        }
    }

    private Type? GetTypeOfValidatorTable(string nameUseCase)
        => ((UseCaseServices)Services).ValidatorsTable[nameUseCase] as Type;

    private Type? GetTypeOfPresenterTable(string nameUseCase)
       => ((UseCaseServices)Services).PresentersTable[nameUseCase] as Type;




    /// <summary>
    /// Permite que um UseCase chame outro UseCase mesmo sem conhecê-lo
    /// </summary>
    /// <typeparam name="TReceiver">UseCase destinatário</typeparam>
    /// <param name="obj">Informação a ser enviada para o UseCase destinatário</param>
    /// <param name="sender">UseCase Remetente</param>
    /// <exception cref="InvalidOperationException">Se um dos tipos necessários (UseCase, Validator, Presenter ou Translater) não tiverem sido adicionados no Mediator</exception>
    /// <remarks>A informação enviada deverá ser tratada por um método Translate a ser configurado no Mediator (RegisterTranslate). De forma que a mesma
    /// possa ser convertida para o Input do UseCase destinatário.</remarks>
    public async Task<IPresenter> HandleAsync<TReceiver>(dynamic obj, IUseCase sender)
        where TReceiver : IUseCase
    {
        var useCaseReceiver = (IUseCase)Factory.Get<TReceiver>();

        useCaseReceiver.SetMediator(this);

        return await useCaseReceiver.HandleAsync(obj);
    }

    public void SetUser(IUser user) => User = user;

    public void Subscribe<TEvent, TUseCaseSender>(IUseCase useCaseObserver)
        where TEvent : IUseCaseEvent
        where TUseCaseSender : IUseCase
        => Observer.Subscribe<TEvent, TUseCaseSender>(useCaseObserver);

    public void Publish(IUseCaseEvent @event)
        => Observer.Publish(@event);
}
