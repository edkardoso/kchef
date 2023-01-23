namespace edk.Fusc.Contracts;

public interface IFactoryMediator
{
    object Get(Type type);
    object Get<T>();
}