using edk.Fusc.Core.Events;
using edk.Fusc.Core.Presenters;

namespace edk.Fusc.Core.Mediator;

public interface IMediatorUseCase
{
    void RegisterTranslate<TTranlaste>(TTranlaste tranlaste, dynamic obj) where TTranlaste : ITranlaste;

    Task<IPresenter> HandleAsync<TReceiver>(dynamic obj, IUseCase sender)
        where TReceiver : IUseCase;
    Task<IPresenter> HandleAsync<TUseCase>(dynamic input) where TUseCase : IUseCase;
    UseCaseServices Services { get; }

    FactoryMediator Factory { get; }

    void SetUser(IUser user);
    void Subscribe<TEvent>(IUseCase useCase) where TEvent : IUseCaseEvent;
 
}

public class MediatorNull : IMediatorUseCase
{
    public UseCaseServices Services => throw new NotImplementedException();

    public FactoryMediator Factory => throw new NotImplementedException();

    public Task<IPresenter> HandleAsync<TReceiver>(dynamic obj, IUseCase sender) where TReceiver : IUseCase
    {
        throw new NotImplementedException();
    }

    public Task<IPresenter> HandleAsync<TUseCase>(dynamic input) where TUseCase : IUseCase
    {
        throw new NotImplementedException();
    }


    public void Publish(IUseCase eventSender, IUseCaseEvent @event)
    {
        throw new NotImplementedException();
    }

    public void RegisterTranslate<TTranlaste>(TTranlaste tranlaste, dynamic obj) where TTranlaste : ITranlaste
    {
        throw new NotImplementedException();
    }

    public void SetUser(IUser user)
    {
        throw new NotImplementedException();
    }

    public void Subscribe<TEvent>(IUseCase useCase) where TEvent : IUseCaseEvent
    {
        throw new NotImplementedException();
    }

   
}