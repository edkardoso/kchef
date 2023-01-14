using edk.Fusc.Core;

namespace edk.Fusc.Core.Events;

public class UseCaseStartEvent : UseCaseEventBase
{
    public UseCaseStartEvent(IUseCase useCase) : base(useCase)
    {
    }
}
