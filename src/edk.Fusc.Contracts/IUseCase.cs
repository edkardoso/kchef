using edk.Fusc.Core.Validators;

namespace edk.Fusc.Contracts;
public interface IUseCase
{
    Task<IPresenter> HandleAsync(dynamic input);
    void SetMediator(IMediatorUseCase mediator);
    void SetValidator(IUseCaseValidator validator);
    void SetPresenter(IPresenter presenter);
    Task OnEventAsync<TEvent>(TEvent useCaseEvent) where TEvent : IUseCaseEvent;

    public bool HasMediator { get; }
    public bool HasValidator { get; }
    public bool HasPresenter { get; }
}
public interface IUseCase<TInput, TOutput> : IUseCase
{
    Task<IPresenter<TInput, TOutput>> HandleAsync(TInput input);

}
