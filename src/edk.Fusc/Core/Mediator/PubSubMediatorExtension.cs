using edk.Fusc.Contracts;
using edk.Fusc.Core.Events;

namespace edk.Fusc.Core.Mediator;

public static class PubSubMediatorExtension
{
    public static void PublishEventStart(this IMediatorUseCase @this, IUseCase useCase, object? input, bool waitComplete)
         => _ = @this.PubSub.PublishAsync(new UseCaseStartEvent(useCase, input, waitComplete));

    public static void PublishEventSuccess(this IMediatorUseCase @this, IUseCase useCase, object? input, object? output, List<INotification> notifications, bool waitComplete)
         => _ = @this.PubSub.PublishAsync(new UseCaseSuccessEvent(useCase, input, output, notifications, waitComplete));

    public static void PublishEventFailureAsync(this IMediatorUseCase @this, IUseCase useCase, object? input, List<Exception> exceptions, bool waitComplete )
         => _ = @this.PubSub.PublishAsync(new UseCaseFailureEvent(useCase, input, exceptions, waitComplete) );

    public static void PublishEventCustom(this IMediatorUseCase @this, IUseCaseEvent @event)
         => _ = @this.PubSub.PublishAsync(@event);
}
