namespace edk.Kchef.Application.Fusc.Events;

public class UseCaseCompleteEvent : UseCaseEventBase
{
    public UseCaseCompleteEvent(IUseCase useCase) : base(useCase)
    {
    }
}
