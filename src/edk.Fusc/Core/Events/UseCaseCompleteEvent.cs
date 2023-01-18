using edk.Fusc.Core.Mediator;

namespace edk.Fusc.Core.Events;

public class UseCaseCompleteEvent : UseCaseEventBase
{
    public UseCaseCompleteEvent(IUseCase useCase, IMediatorUseCase mediator) 
        : base(useCase, mediator)
    {}
}
