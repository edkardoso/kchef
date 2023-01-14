namespace edk.Fusc.Core.Events;

public class UseCaseSuccessEvent : UseCaseEventBase
{
    public UseCaseSuccessEvent(IUseCase useCase) : base(useCase)
    {
    }
}
