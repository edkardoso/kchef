namespace edk.Fusc.Core.Events;

public class UseCaseErrorEvent : UseCaseEventBase
{
    public UseCaseErrorEvent(IUseCase useCase) : base(useCase)
    {
    }
}
