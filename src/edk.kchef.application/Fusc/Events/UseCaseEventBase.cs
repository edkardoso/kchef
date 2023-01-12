using System;

namespace edk.Kchef.Application.Fusc.Events;

public abstract class UseCaseEventBase : IUseCaseEvent
{
    protected UseCaseEventBase(IUseCase useCase)
    {
        Sender = useCase.GetType();
    }
    public Type Sender { get; private set; }
}
