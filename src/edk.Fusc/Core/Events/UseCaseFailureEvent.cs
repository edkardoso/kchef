using edk.Fusc.Contracts;
using edk.Fusc.Contracts.Common;

namespace edk.Fusc.Core.Events;

public class UseCaseFailureEvent : UseCaseEventBase
{
    public UseCaseFailureEvent(IUseCase useCase) : base(useCase)
    {
        Category = UseCaseEventCategory.Failure;
    }

    public List<Exception> Exceptions { get; internal set; } = new List<Exception> { };
}
