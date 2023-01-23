using edk.Fusc.Contracts;
using edk.Fusc.Core.Mediator;

namespace edk.Fusc.Core.Events;

public class UseCaseStartEvent : UseCaseEventBase
{
    public UseCaseStartEvent(IUseCase useCase) 
        : base(useCase)
    {}
}
