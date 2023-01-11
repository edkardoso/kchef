using System.Threading.Tasks;
using FluentValidation;

namespace edk.Kchef.Application.Fusc;

public interface IMediatorUseCase
{
    void AddUseCase<TService, TInput, TOutput>() where TService : IUseCase<TInput, TOutput>;

    void AddTranslate<TTranlaste>(TTranlaste tranlaste, dynamic obj) where TTranlaste : ITranlaste;

    Task<IPresenter<TInput, TOutput>> SendAsync<TReceiver, TInput, TOutput>(dynamic obj, IUseCase<TInput, TOutput> sender)
        where TReceiver : IUseCase<TInput, TOutput>;
    Task<IPresenter<TInput, TOutput>> HandleAsync<TUseCase, TInput, TOutput>(TInput input) where TUseCase : IUseCase<TInput, TOutput>;
    void AddUseCase<TService, TValidator, TInput, TOutput>()
        where TService : IUseCase<TInput, TOutput>
        where TValidator : IValidator<TInput>;
    void AddUseCase<TService, TValidator, TPresenter, TInput, TOutput>()
        where TService : IUseCase<TInput, TOutput>
        where TValidator : IValidator<TInput>
        where TPresenter : IPresenter<TInput, TOutput>;
}
