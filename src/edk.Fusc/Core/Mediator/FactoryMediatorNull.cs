using edk.Fusc.Contracts;

namespace edk.Fusc.Core.Mediator;

public class FactoryMediatorNull : IFactoryMediator
{
    public object Get(Type type)
    {
        throw new NotImplementedException();
    }

    public object Get<T>()
    {
        throw new NotImplementedException();
    }
}
