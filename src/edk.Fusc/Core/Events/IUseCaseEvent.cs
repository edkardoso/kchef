namespace edk.Fusc.Core.Events;

public interface IUseCaseEvent
{
    Type Sender { get; }
    string Name { get; }
    DateTime? StartDate { get; }
}
