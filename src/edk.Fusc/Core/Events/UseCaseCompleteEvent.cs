namespace edk.Fusc.Core.Events;

public class UseCaseCompleteEvent : UseCaseEventBase
{
    public UseCaseCompleteEvent(IUseCase useCase) : base(useCase)
    {
    }
}
