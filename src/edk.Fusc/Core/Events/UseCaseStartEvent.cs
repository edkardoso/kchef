using edk.Fusc.Contracts;
using edk.Fusc.Contracts.Common;

namespace edk.Fusc.Core.Events;

public class UseCaseStartEvent : UseCaseEventBase
{
    public UseCaseStartEvent(IUseCase useCase) 
        : base(useCase)
    {
        Category = UseCaseEventCategory.Start;
    }

    public dynamic Input { get; set; }
}
