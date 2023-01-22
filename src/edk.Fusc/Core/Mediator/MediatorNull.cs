using edk.Fusc.Contracts;
using edk.Fusc.Core.Events;
using edk.Fusc.Core.Presenters;

namespace edk.Fusc.Core.Mediator;

public class MediatorNull : IMediatorUseCase
{
    public IUseCaseServices Services => new UseCaseServicesNull();

    public IFactoryMediator Factory => new FactoryMediatorNull();

    public IUser User => new UserNull();

    public Task<IPresenter> HandleAsync<TReceiver>(dynamic obj, IUseCase sender) where TReceiver : IUseCase
    {
        throw new NotImplementedException();
    }

    public Task<IPresenter> HandleAsync<TUseCase>(dynamic input) where TUseCase : IUseCase
    {
        throw new NotImplementedException();
    }

    public void Publish(IUseCaseEvent @event)
    {
        throw new NotImplementedException();
    }

    public void SetUser(IUser user)
    {
        throw new NotImplementedException();
    }

    public void Subscribe<TEvent, TUseCaseSender>(IUseCase useCaseObserver)
        where TEvent : IUseCaseEvent
        where TUseCaseSender : IUseCase
    {
        throw new NotImplementedException();
    }
}