using edk.Fusc.Contracts;

namespace edk.Fusc.Core.Mediator;

internal class UseCaseMediatorNull : IMediatorUseCase
{
    public IUseCaseServices Services => new UseCaseServicesExtensionNull();

    public IFactoryMediator Factory => new FactoryMediatorNull();

    public IPubSubMediator PubSub => new PubSubMediator(Factory);

    public IUser User => new UserNull();

    public TUseCase GetInstance<TUseCase>()
    {
        throw new NotImplementedException();
    }

    public Task<IPresenter> HandleAsync<TReceiver>(dynamic obj, IUseCase sender) where TReceiver : IUseCase
    {
        throw new NotImplementedException();
    }

    public Task<IPresenter> HandleAsync<TUseCase>(dynamic input) where TUseCase : IUseCase
    {
        throw new NotImplementedException();
    }

   
    public void SetUser(IUser user)
    {
        throw new NotImplementedException();
    }

  
}