namespace edk.Kchef.Application.Fusc.Events;

public class UseCaseStartEvent : UseCaseEventBase
{
    public UseCaseStartEvent(IUseCase useCase) : base(useCase)
    {
    }
}
