using System;
using Microsoft.Extensions.DependencyInjection;

namespace edk.Kchef.Application.Fusc;

public class FactoryMediator
{
    private readonly IServiceProvider _provider;

    public FactoryMediator(IServiceProvider Provider)
    {
        _provider = Provider;
    }

    public object Get<T>()
        => _provider.GetRequiredService(typeof(T));

    public object Get(Type type)
       => _provider.GetRequiredService(type);
}
