using edk.Fusc.Contracts;

namespace edk.Fusc.Core.Mediator;

public static class BuilderKey
{
    public static string ByEvent(IUseCaseEvent @event)
      => Builder(@event.SenderType, @event.GetType());

    public static string BySender(Type senderType, Type eventType)
     => Builder(senderType, eventType);

    private static string Builder(Type senderType, Type eventType)
        => $"{senderType.Name}_{eventType.Name}";
}
