namespace edk.Fusc.Core.Mediator;

public struct ObserverUseCase
{
    public ObserverUseCase(IUseCase useCase, Type typeEvent)
        :this(GetNameEvent(useCase, typeEvent), useCase)
    {}

    public ObserverUseCase(string key, IUseCase useCase)
    {
        Key = key;
        UseCase = useCase;
    }

    public string Key { get; }
    public IUseCase UseCase { get; }

    private static string GetNameEvent(IUseCase observer, Type typeEvent)
      => observer.GetType().Name + typeEvent.GetType().Name;
}
