using edk.Tools;

namespace edk.Fusc.Contracts;

public interface IPresenter
{
    bool Success { get; }
    bool Fail => !Success;
    dynamic ViewOutput { get; }
    IOption<dynamic> Output { get; }

}
public interface IPresenter<TInput, TOutput> : IPresenter
{
    void OnErrorValidation(TInput input, IReadOnlyCollection<INotification> notifications);

    void OnError(Exception exception, TInput input);

    void OnResult(TOutput output, IReadOnlyCollection<INotification> notifications, CancellationToken cancellationToken);

    new IOption<TOutput> Output { get; }
    void SetOutput(TOutput output);

    void SetSuccess(bool value);


}
