using edk.Fusc.Core.Events;
using edk.Fusc.Core.Presenters;

namespace edk.Fusc.Core.Mediator;

public interface IMediatorUseCase
{
    void RegisterTranslate<TTranlaste>(TTranlaste tranlaste, dynamic obj) where TTranlaste : ITranlaste;

    Task<IPresenter> HandleAsync<TReceiver>(dynamic obj, IUseCase sender)
        where TReceiver : IUseCase;
    Task<IPresenter> HandleAsync<TUseCase>(dynamic input) where TUseCase : IUseCase;
    IUseCaseServices Services { get; }

    IFactoryMediator Factory { get; }
    IUser User { get; }

    void SetUser(IUser user);

    void Subscribe<TEvent, TUseCaseSender>(IUseCase useCaseObserver)
        where TEvent : IUseCaseEvent
        where TUseCaseSender : IUseCase;



}
