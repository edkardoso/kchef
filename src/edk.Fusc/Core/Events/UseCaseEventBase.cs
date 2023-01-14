namespace edk.Fusc.Core.Events;

public abstract class UseCaseEventBase : IUseCaseEvent
{
    protected UseCaseEventBase(IUseCase useCase)
    {
        Sender = useCase.GetType();
        StartDate = DateTime.Now;
    }
    public Type Sender { get; private set; }

    public string Name { get; private set; }

    public DateTime? StartDate { get; private set; }
}
