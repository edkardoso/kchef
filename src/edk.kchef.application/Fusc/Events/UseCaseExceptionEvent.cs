namespace edk.Kchef.Application.Fusc.Events;

public class UseCaseExceptionEvent : UseCaseEventBase
{
    public UseCaseExceptionEvent(IUseCase useCase) : base(useCase)
    {
    }
}
