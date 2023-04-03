using edk.Fusc.Contracts;
using edk.Fusc.Contracts.Common;

namespace edk.Fusc.Core.Events;

public abstract class UseCaseEventBase : IUseCaseEvent
{
    protected UseCaseEventBase(IUseCase useCaseSender)
    {
        SenderType = useCaseSender.GetType();
        StartDate = DateTime.Now;
        Category = UseCaseEventCategory.Custom;
    }
    public Type SenderType { get; }

    public DateTime? StartDate { get;  }

    public UseCaseEventCategory Category { get; protected set; }
}
