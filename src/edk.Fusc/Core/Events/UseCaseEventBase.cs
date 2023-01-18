using edk.Fusc.Core.Mediator;

namespace edk.Fusc.Core.Events;

public abstract class UseCaseEventBase : IUseCaseEvent
{
    protected UseCaseEventBase(IUseCase useCaseSender, IMediatorUseCase mediator)
    {
        Sender = useCaseSender.GetType();
        StartDate = DateTime.Now;
        Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    public Type Sender { get; }

    public string Name { get; protected set; } = String.Empty;

    public DateTime? StartDate { get;  }
    public IMediatorUseCase Mediator { get; }

    public void Subscribe(IUseCase useCaseObserver) => Mediator.Subscribe(useCaseObserver, this);
}
