﻿using edk.Fusc.Contracts.Common;

namespace edk.Fusc.Contracts;

public interface IMediatorUseCase : IFuscObject
{
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
    void Publish(IUseCaseEvent @event);
    
}
