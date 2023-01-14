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
}
