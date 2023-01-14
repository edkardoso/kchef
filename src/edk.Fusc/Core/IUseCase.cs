using edk.Fusc.Core.Events;
using edk.Fusc.Core.Mediator;
using edk.Fusc.Core.Presenters;

namespace edk.Fusc.Core
{
    public interface IUseCase
    {
        Task<IPresenter> HandleAsync(dynamic input);

        void SetMediator(IMediatorUseCase mediator);
        Task OnEventAsync(IUseCaseEvent useCaseEvent);
    }
    public interface IUseCase<TInput, TOutput> : IUseCase
    {
        Task<IPresenter<TInput, TOutput>> HandleAsync(TInput input);

    }
}
