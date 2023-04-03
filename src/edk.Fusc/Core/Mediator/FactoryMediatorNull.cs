using edk.Fusc.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace edk.Fusc.Core.Mediator;

internal class FactoryMediatorNull : IFactoryMediator
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
