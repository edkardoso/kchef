﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using edk.Kchef.Domain.Common.Extensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace edk.Kchef.Application.Fusc;

public class UseCaseMediator : IMediatorUseCase
{
    private readonly IServiceCollection _services;
    private readonly Dictionary<string, Type> _translateDictionary = new Dictionary<string, Type>();

    protected FactoryMediator Factory { get; private set; }
    public UseCaseMediator(IServiceCollection services)
    {
        _services = services;
    }

    public UseCaseMediator AddScoped<TService>() where TService : IUseCase
        => AddScoped(typeof(TService));

    public UseCaseMediator AddScoped<TService, TValidator, TInput, TOutput>()
        where TService : IUseCase<TInput, TOutput>
        where TValidator : IValidator<TInput>
        => AddScoped(typeof(TService))
            .AddScoped(typeof(TValidator));

    public UseCaseMediator AddScoped<TService, TValidator, TPresenter>()
       where TService : IUseCase
       where TValidator : IValidator
       where TPresenter : IPresenter
        => AddScoped(typeof(TService))
        .AddScoped(typeof(TValidator))
        .AddScoped(typeof(TPresenter));

    public void Builder()
        => Factory = new FactoryMediator(_services.BuildServiceProvider());

    /// <summary>
    /// Obtém uma instância do UseCase e o executa
    /// </summary>
    public async Task<IPresenter> HandleAsync<TUseCase>(dynamic input)
       where TUseCase : IUseCase
    {
        var useCase = (TUseCase)Factory.Get<TUseCase>();

        useCase.SetMediator(this);

        return await useCase.HandleAsync(input);
    }

    /// <summary>
    /// Permite que um UseCase chame outro UseCase mesmo sem conhecê-lo
    /// </summary>
    /// <typeparam name="TReceiver">UseCase destinatário</typeparam>
    /// <param name="obj">Informação a ser enviada para o UseCase destinatário</param>
    /// <param name="sender">UseCase Remetente</param>
    /// <exception cref="InvalidOperationException">Se um dos tipos necessários (UseCase, Validator, Presenter ou Translater) não tiverem sido adicionados no Mediator</exception>
    /// <remarks>A informação enviada deverá ser tratada por um método Translate a ser configurado no Mediator (RegisterTranslate). De forma que a mesma
    /// possa ser convertida para o Input do UseCase destinatário.</remarks>
    public async Task<IPresenter> HandleAsync<TReceiver>(dynamic obj, IUseCase sender)
        where TReceiver : IUseCase
    {
        var translater = GetTranslater(sender.GetType(), typeof(TReceiver), obj);

        var inputReceiver = translater.Convert(obj);

        var useCaseReceiver = (IUseCase)Factory.Get<TReceiver>();

        useCaseReceiver.SetMediator(this);

        return await useCaseReceiver.HandleAsync(inputReceiver);

    }

    public void RegisterTranslate<TTranslaste>(TTranslaste translaste, dynamic obj)
       where TTranslaste : ITranlaste
    {
        AddScoped(typeof(TTranslaste));

        string key = UseCaseMediatorExtension.GetNameTranslate(translaste.Sender, translaste.Receiver, obj);

        _translateDictionary.AddIfNotContains(key, translaste.GetType());

    }

    private UseCaseMediator AddScoped(Type type)
    {
        _services.AddScoped(type);
        return this;
    }

    private ITranlaste GetTranslater(Type sender, Type receiver, dynamic obj)
    {
        string key = UseCaseMediatorExtension.GetNameTranslate(sender, receiver, obj);

        var translateType = _translateDictionary[key];

        return (ITranlaste)Factory.Get(translateType);
    }
}
