using edk.Fusc.Core.Events;

namespace edk.Fusc.Core.Mediator;

public class ObserverMediator
{
    private ObserverCollection _observers = new();
    public void Subscribe<TEvent, TUseCaseSender>(IUseCase useCaseObserver)
      where TEvent : IUseCaseEvent
      where TUseCaseSender : IUseCase 
        => _observers.Add(useCaseObserver, typeof(TEvent), typeof(TUseCaseSender));

    public void Publish(IUseCaseEvent @event)
        => _observers.Filter(@event)
            .ToList()
            .ForEach(o => o.Observer.OnEventAsync(@event));

    public void Clear()
        => _observers = new();

    public int Count()
        => _observers.Count();
}
