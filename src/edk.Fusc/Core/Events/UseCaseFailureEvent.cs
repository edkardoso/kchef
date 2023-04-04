using edk.Fusc.Contracts;
using edk.Fusc.Contracts.Common;

namespace edk.Fusc.Core.Events;

public class UseCaseFailureEvent : UseCaseEventBase
{
    public List<Exception> Exceptions { get; internal set; }
    public object? Input { get; }

    public UseCaseFailureEvent(IUseCase useCase, object? input, List<Exception> exceptions, bool waitComplete) : base(useCase)
    {
        Category = UseCaseEventCategory.Failure;
        Input = input;
        Exceptions = exceptions;
        WaitingCompletion = waitComplete;
    }


}
