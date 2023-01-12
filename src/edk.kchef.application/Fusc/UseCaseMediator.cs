using System;
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
    private IServiceProvider _provider;

    public IServiceProvider Provider => _provider ??= _services.BuildServiceProvider();

    public UseCaseMediator(IServiceCollection services)
    {
        _services = services;
    }

    public void AddTranslate<TTranslaste>(TTranslaste translaste, dynamic obj)
        where TTranslaste : ITranlaste
    {
        _services.AddScoped(typeof(TTranslaste));

        string key = UseCaseMediatorExtension.GetNameTranslate(translaste.Sender, translaste.Receiver, obj);

        _translateDictionary.AddIfNotContains(key, translaste.GetType());

    }

    /// <summary>
    /// Adiciona um UseCase no escopo de DI
    /// </summary>
    /// <typeparam name="TService">Entidade/UseCase a ser adicionado</typeparam>
    /// <typeparam name="TInput">Modelo de entrada do UseCase</typeparam>
    /// <typeparam name="TOutput">Modelo de Saida do UseCase</typeparam>
    public void AddUseCase<TService, TInput, TOutput>() where TService : IUseCase<TInput, TOutput>
        => _services.AddScoped(typeof(TService));

    public void AddUseCase<TService, TValidator, TInput, TOutput>()
        where TService : IUseCase<TInput, TOutput>
        where TValidator : IValidator<TInput>
    {
        _services.AddScoped(typeof(TService));
        _services.AddScoped(typeof(TValidator));
    }

    public void AddUseCase<TService, TValidator, TPresenter, TInput, TOutput>()
       where TService : IUseCase<TInput, TOutput>
       where TValidator : IValidator<TInput>
       where TPresenter : IPresenter<TInput, TOutput>
    {
        _services.AddScoped(typeof(TService));
        _services.AddScoped(typeof(TValidator));
        _services.AddScoped(typeof(TPresenter));
    }


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TReceiver"></typeparam>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TOutput"></typeparam>
    /// <param name="obj"></param>
    /// <param name="sender"></param>
    /// <returns></returns>
    public async Task<IPresenter> SendAsync<TReceiver>(dynamic obj, IUseCase sender)
        where TReceiver : IUseCase
    {

        var translater = GetTranslater(sender.GetType(), typeof(TReceiver), obj);

        var inputReceiver = translater.Convert(obj);

        var useCaseReceiver = (IUseCase)Provider.GetRequiredService(typeof(TReceiver));

        return await useCaseReceiver.HandleAsync(inputReceiver);

    }

    public async Task<IPresenter> HandleAsync<TUseCase>(dynamic input)
        where TUseCase : IUseCase
    {
        var useCase = (TUseCase)Provider.GetRequiredService(typeof(TUseCase));

        var presenter = await useCase.HandleAsync(input);

        return presenter;
    }

    private ITranlaste GetTranslater(Type sender, Type receiver, dynamic obj)
    {
        string key = UseCaseMediatorExtension.GetNameTranslate(sender, receiver, obj);

        var translateType = _translateDictionary[key];

        return (ITranlaste)Provider.GetRequiredService(translateType);
    }
}
