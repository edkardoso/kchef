﻿using edk.Fusc.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace edk.Fusc.Core.Mediator;

public class FactoryMediator : IFactoryMediator
{
    private readonly IServiceProvider _provider;

    // To Unit Tests
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    internal protected FactoryMediator() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    internal FactoryMediator(IServiceProvider provider)
    {
        _provider = provider;
    }

    public virtual object Get<T>()
        => _provider.GetRequiredService(typeof(T));

    public virtual object Get(Type type)
       => _provider.GetRequiredService(type);
}
