using edk.Fusc.Contracts;
using edk.Fusc.Contracts.Common;

namespace edk.Fusc.Core.Events;

public class UseCaseStartEvent : UseCaseEventBase
{
    public UseCaseStartEvent(IUseCase useCase, object? input) 
        : base(useCase)
    {
        Category = UseCaseEventCategory.Start;
        Input = input;
    }

    public object? Input { get;  }
}
