using edk.Fusc.Contracts.Common;

namespace edk.Fusc.Contracts;

public interface IMediatorUseCase : IFuscObject, INullableObject
{
    Task<IPresenter> HandleAsync<TReceiver>(dynamic obj, IUseCase sender)
        where TReceiver : IUseCase;
    Task<IPresenter> HandleAsync<TUseCase>(dynamic input) where TUseCase : IUseCase;
    IUseCaseServices Services { get; }

    IFactoryMediator Factory { get; }

    IPubSubMediator PubSub { get; }

    IUser User { get; }

    void SetUser(IUser user);


   
}
