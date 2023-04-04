using edk.Fusc.Contracts.Common;

namespace edk.Fusc.Contracts;
public interface IUseCaseEvent
{
    Type SenderType { get; }
    DateTime? StartDate { get; }
    UseCaseEventCategory Category { get; }
    bool WaitingCompletion { get; }
}


