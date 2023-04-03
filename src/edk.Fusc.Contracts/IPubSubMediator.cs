namespace edk.Fusc.Contracts
{
    public interface IPubSubMediator
    {
        Dictionary<string, List<Type>> Subscriptions { get; }
        Task PublishAsync(IUseCaseEvent @event);
        void SubscribeFrom<TSender, TEvent>(IUseCase recipient)
            where TSender : IUseCase
            where TEvent : IUseCaseEvent;
        void SubscribeTo<TRecipient, TEvent>(IUseCase sender)
            where TRecipient : IUseCase
            where TEvent : IUseCaseEvent;
    }
}