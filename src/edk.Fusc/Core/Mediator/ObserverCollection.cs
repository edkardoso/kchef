namespace edk.Fusc.Core.Mediator;

public class ObserverCollection
{
    private readonly List<ObserverUseCase> _observers = new();

    public void Add(IUseCase useCase, Type typeEvent)
    {
        var observer = new ObserverUseCase(useCase, typeEvent);

        var notExists = !_observers.Exists(o => o.Key.Equals(observer.Key));

        if (notExists)
            _observers.Add(observer);
    }

    public void Remove(ObserverUseCase observer)
        => _observers.Remove(observer);
}
