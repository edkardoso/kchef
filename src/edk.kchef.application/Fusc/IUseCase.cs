using edk.Kchef.Application.Fusc.Events;
using edk.Kchef.Application.Fusc.Mediator;
using edk.Kchef.Application.Fusc.Presenters;
using MediatR;
using System;
using System.Threading.Tasks;

namespace edk.Kchef.Application.Fusc
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
