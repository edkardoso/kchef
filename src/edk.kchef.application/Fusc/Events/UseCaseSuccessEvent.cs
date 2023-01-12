namespace edk.Kchef.Application.Fusc.Events;

public class UseCaseSuccessEvent : UseCaseEventBase
{
    public UseCaseSuccessEvent(IUseCase useCase) : base(useCase)
    {
    }
}
