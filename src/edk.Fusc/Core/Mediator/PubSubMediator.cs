using edk.Fusc.Contracts;
using edk.Tools.NoIf.Miscellaneous;

namespace edk.Fusc.Core.Mediator;
internal class PubSubMediator : IPubSubMediator
{
    private readonly IFactoryMediator _factory;

    public Dictionary<string, List<Type>> Subscriptions { get; private set; } = new();

    public PubSubMediator(IFactoryMediator factory)
    {
        _factory = factory;
    }

    public void SubscribeFrom<TSender, TEvent>(IUseCase recipient)
       where TSender : IUseCase
       where TEvent : IUseCaseEvent
    {
        var key = BuilderKey.BySender(typeof(TSender), typeof(TEvent));

        AddSubscriptions(key, recipient.GetType());
    }

    public void SubscribeTo<TRecipient, TEvent>(IUseCase sender)
        where TRecipient : IUseCase
        where TEvent : IUseCaseEvent
    {
        var key = BuilderKey.BySender(sender.GetType(), typeof(TEvent));

        AddSubscriptions(key, typeof(TRecipient));
    }

    private void AddSubscriptions(string key, Type recipientType)
    {
        List<Type> collectionOfRecipients = Subscriptions.ContainsKey(key) ? Subscriptions[key] : new();

        collectionOfRecipients.Add(recipientType);

        Subscriptions.Add(key, collectionOfRecipients);
    }


    public async Task PublishAsync(IUseCaseEvent @event)
    {
        var senderEvent = BuilderKey.ByEvent(@event);

        if (Subscriptions.ContainsKey(senderEvent).Not())
            return;

        var collectionOfRecipients = Subscriptions[senderEvent];

        var notifyTask = NotifyAsync(@event, collectionOfRecipients);

        if (@event.WaitingCompletion)
            await Task.WhenAll(notifyTask);
    }

    private IEnumerable<Task> NotifyAsync(IUseCaseEvent @event, List<Type> recipients)
    {
        foreach (var recipientType in recipients)
        {
            var usecase = (IUseCase)_factory.Get(recipientType);

            yield return usecase.OnEventAsync(@event);
        }
    }
}
