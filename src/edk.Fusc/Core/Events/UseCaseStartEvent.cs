using edk.Fusc.Contracts;
using edk.Fusc.Contracts.Common;

namespace edk.Fusc.Core.Events;

public class UseCaseStartEvent : UseCaseEventBase
{
    public object? Input { get;  }

    public UseCaseStartEvent(IUseCase useCase, object? input, bool waitComplete) 
        : base(useCase)
    {
        Category = UseCaseEventCategory.Start;
        Input = input;
        WaitingCompletion = waitComplete;
    }
}
