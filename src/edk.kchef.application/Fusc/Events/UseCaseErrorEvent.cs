namespace edk.Kchef.Application.Fusc.Events;

public class UseCaseErrorEvent : UseCaseEventBase
{
    public UseCaseErrorEvent(IUseCase useCase) : base(useCase)
    {
    }
}
