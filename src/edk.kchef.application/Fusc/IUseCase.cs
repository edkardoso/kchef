using MediatR;
using System;
using System.Threading.Tasks;

namespace edk.Kchef.Application.Fusc
{
    public interface IUseCase
    {
        Task<IPresenter> HandleAsync(dynamic input);
    }
    public interface IUseCase<TInput, TOutput> : IUseCase
    {
        void Handle(IUseCase<TInput, TOutput> other);
        Task<IPresenter<TInput, TOutput>> HandleAsync(TInput input);

    }
}
