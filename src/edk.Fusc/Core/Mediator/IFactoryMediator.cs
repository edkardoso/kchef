namespace edk.Fusc.Core.Mediator
{
    public interface IFactoryMediator
    {
        object Get(Type type);
        object Get<T>();
    }
}