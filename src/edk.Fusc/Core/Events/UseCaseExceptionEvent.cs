using edk.Fusc.Core;

namespace edk.Fusc.Core.Events;

public class UseCaseExceptionEvent : UseCaseEventBase
{
    public UseCaseExceptionEvent(IUseCase useCase) : base(useCase)
    {
    }
}
