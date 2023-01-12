using System.Threading.Tasks;
using FluentValidation;

namespace edk.Kchef.Application.Fusc;

public interface IMediatorUseCase
{

    void RegisterTranslate<TTranlaste>(TTranlaste tranlaste, dynamic obj) where TTranlaste : ITranlaste;

    Task<IPresenter> HandleAsync<TReceiver>(dynamic obj, IUseCase sender)
        where TReceiver : IUseCase;
    Task<IPresenter> HandleAsync<TUseCase>(dynamic input) where TUseCase : IUseCase;
    UseCaseMediator AddScoped<TService>() where TService : IUseCase;
    UseCaseMediator AddScoped<TService, TValidator, TInput, TOutput>()
        where TService : IUseCase<TInput, TOutput>
        where TValidator : IValidator<TInput>;
    UseCaseMediator AddScoped<TService, TValidator, TPresenter>()
        where TService : IUseCase
        where TValidator : IValidator
        where TPresenter : IPresenter;
}
