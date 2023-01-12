using System.Threading.Tasks;
using FluentValidation;

namespace edk.Kchef.Application.Fusc;

public interface IMediatorUseCase
{
    void AddUseCase<TService, TInput, TOutput>() where TService : IUseCase<TInput, TOutput>;

    void AddTranslate<TTranlaste>(TTranlaste tranlaste, dynamic obj) where TTranlaste : ITranlaste;

    Task<IPresenter> SendAsync<TReceiver>(dynamic obj, IUseCase sender)
        where TReceiver : IUseCase;
    Task<IPresenter> HandleAsync<TUseCase>(dynamic input) where TUseCase : IUseCase;
    void AddUseCase<TService, TValidator, TInput, TOutput>()
        where TService : IUseCase<TInput, TOutput>
        where TValidator : IValidator<TInput>;
    void AddUseCase<TService, TValidator, TPresenter, TInput, TOutput>()
        where TService : IUseCase<TInput, TOutput>
        where TValidator : IValidator<TInput>
        where TPresenter : IPresenter<TInput, TOutput>;
}
