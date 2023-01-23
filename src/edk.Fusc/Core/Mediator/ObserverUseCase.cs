using edk.Fusc.Contracts;

namespace edk.Fusc.Core.Mediator;

public struct ObserverUseCase
{
    public ObserverUseCase(IUseCase useCaseObserver, Type typeEvent, Type typeSender )
    {
        Sender = typeSender;
        Event = typeEvent;
        Observer = useCaseObserver;
    }

    public Type Sender { get; }
    public Type Event { get; }
    public IUseCase Observer { get; }

    private static string GetNameEvent(IUseCase observer, Type typeEvent)
      => observer.GetType().Name + typeEvent.GetType().Name;
}
