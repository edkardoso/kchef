using edk.Fusc.Contracts;
using edk.Fusc.Contracts.Common;

namespace edk.Fusc.Core.Events;

public class UseCaseSuccessEvent : UseCaseEventBase
{
    public UseCaseSuccessEvent(IUseCase useCase):base(useCase)
    {
        Category = UseCaseEventCategory.Success;
    }
}
