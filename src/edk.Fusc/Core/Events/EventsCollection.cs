using edk.Fusc.Contracts;

namespace edk.Fusc.Core.Events;

public class EventsCollection
{
    private List<IUseCaseEvent> _events= new();

    public void Add(IUseCaseEvent @event)
        => _events.Add(@event);

   
}
