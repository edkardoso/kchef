using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using edk.Kchef.Domain.Common.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace edk.Kchef.Application.Fusc;

public class UseCaseMediator : IMediatorUseCase
{
    
    private readonly Dictionary<string, Type> _translateDictionary = new();

    public FactoryMediator Factory { get; private set; }
    public UseCaseServices Services { get; private set; }
    public UseCaseMediator(IServiceCollection services)
    {
        Services =  new UseCaseServices(services);
    }

    public void Builder()
        => Factory = new FactoryMediator(Services.BuildServiceProvider());

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
        Services.AddScoped(typeof(TTranslaste));

        string key = UseCaseMediatorExtension.GetNameTranslate(translaste.Sender, translaste.Receiver, obj);

        _translateDictionary.AddIfNotContains(key, translaste.GetType());

    }

    private ITranlaste GetTranslater(Type sender, Type receiver, dynamic obj)
    {
        string key = UseCaseMediatorExtension.GetNameTranslate(sender, receiver, obj);

        var translateType = _translateDictionary[key];

        return (ITranlaste)Factory.Get(translateType);
    }
}
