namespace edk.Fusc.Contracts;

public interface IUseCase
{
    Task<IPresenter> HandleAsync(dynamic input);

    void SetMediator(IMediatorUseCase mediator);
    Task OnEventAsync<TEvent>(TEvent useCaseEvent) where TEvent : IUseCaseEvent;
}
public interface IUseCase<TInput, TOutput> : IUseCase
{
    Task<IPresenter<TInput, TOutput>> HandleAsync(TInput input);

}
