using edk.Fusc.Contracts;

namespace edk.Fusc.Core.Events;

public abstract class UseCaseEventBase : IUseCaseEvent
{
    protected UseCaseEventBase(IUseCase useCaseSender)
    {
        Sender = useCaseSender.GetType();
        StartDate = DateTime.Now;
    }
    public Type Sender { get; }

    public DateTime? StartDate { get;  }

}
