namespace edk.Fusc.Contracts;

public interface IUseCaseEvent
{
    Type Sender { get; }
    DateTime? StartDate { get; }


}


