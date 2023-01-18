using edk.Fusc.Core;
using edk.Fusc.Core.Mediator;

namespace edk.Fusc.Core.Events;

public class UseCaseStartEvent : UseCaseEventBase
{
    public UseCaseStartEvent(IUseCase useCase, IMediatorUseCase mediator) 
        : base(useCase, mediator)
    {}
}
