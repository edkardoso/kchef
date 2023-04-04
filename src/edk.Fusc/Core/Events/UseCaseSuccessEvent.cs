using edk.Fusc.Contracts;
using edk.Fusc.Contracts.Common;

namespace edk.Fusc.Core.Events;

public class UseCaseSuccessEvent : UseCaseEventBase
{
    public object? Input { get; }
    public object? Output { get;  }
    public List<INotification> Notifications { get; }


    public UseCaseSuccessEvent(IUseCase useCase, object? input, object? output, List<INotification> notifications, bool waitComplete) :base(useCase)
    {
        Category = UseCaseEventCategory.Success;
        Input = input;
        Output = output;
        Notifications = notifications;
        WaitingCompletion = waitComplete;
    }
}
