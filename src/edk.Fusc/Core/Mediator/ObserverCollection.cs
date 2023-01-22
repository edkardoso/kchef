using edk.Fusc.Core.Events;

namespace edk.Fusc.Core.Mediator;

public class ObserverCollection
{
    private readonly List<ObserverUseCase> _observers = new();

    public void Add(IUseCase useCaseObserver, Type typeEvent, Type typeUseCaseSender)
    {
        var observerNew = new ObserverUseCase(useCaseObserver, typeEvent, typeUseCaseSender);

        var notExists = !_observers.Exists(o => o.Sender.Equals(observerNew.Sender) 
                                            && o.Event.Equals(observerNew.Event)
                                            && o.Observer.GetType().Equals(observerNew.GetType()));
        if (notExists)
            _observers.Add(observerNew);
    }

    public void Remove(ObserverUseCase observer)
        => _observers.Remove(observer);


    public  IEnumerable<ObserverUseCase> Filter(IUseCaseEvent @event) 
        => _observers.Where(o => o.Sender.Equals(@event.Sender) && o.Event.Equals(@event.GetType()));

    public int Count()
        => _observers.Count();
}
