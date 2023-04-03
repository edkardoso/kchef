using edk.Fusc.Contracts;
using edk.Fusc.Core.Validators;
using edk.Tools.NoIf.Miscellaneous;
using Microsoft.Extensions.DependencyInjection;

namespace edk.Fusc.Core.Mediator;

public class UseCaseMediator : IMediatorUseCase
{

    public virtual IFactoryMediator Factory { get; private set; }

    public IPubSubMediator PubSub { get; private set; }

    public IUseCaseServices Services { get; private set; }
    public IUser User { get; private set; }

    public bool IsProduction { get; private set; }
    public bool IsDevelopment => IsProduction.Not();

    public UseCaseMediator(IFactoryMediator factory) : this(new UseCaseServicesNull(), factory)
    { }

    public UseCaseMediator(IUseCaseServices services, IFactoryMediator factory)
    {
        Services = services;
        Factory = factory;
        User = new UserNull();
        PubSub = new PubSubMediator(Factory);
    }

    public UseCaseMediator(IServiceCollection services)
    {
        Factory = new FactoryMediatorNull();
        Services = new UseCaseServicesExtension(services);
        User = new UserNull();
        PubSub = new PubSubMediator(Factory);
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
        => ((UseCaseServicesExtension)Services).ValidatorsTable[nameUseCase] as Type;

    private Type? GetTypeOfPresenterTable(string nameUseCase)
       => ((UseCaseServicesExtension)Services).PresentersTable[nameUseCase] as Type;




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


}
