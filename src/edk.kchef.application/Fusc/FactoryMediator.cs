using System;
using Microsoft.Extensions.DependencyInjection;

namespace edk.Kchef.Application.Fusc;

public class FactoryMediator
{
    private readonly IServiceProvider _provider;
    
    // To Unit Tests
    protected FactoryMediator()
    {

    }

    public FactoryMediator(IServiceProvider Provider)
    {
        _provider = Provider;
    }

    public virtual object Get<T>()
        => _provider.GetRequiredService(typeof(T));

    public virtual object Get(Type type)
       => _provider.GetRequiredService(type);
}
