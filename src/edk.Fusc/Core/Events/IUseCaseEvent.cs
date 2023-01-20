namespace edk.Fusc.Core.Events;

public interface IUseCaseEvent
{
    Type Sender { get; }
    DateTime? StartDate { get; }

  
}


