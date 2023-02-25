using edk.Fusc.Contracts.Common;
using edk.Tools;

namespace edk.Fusc.Contracts;

public interface IPresenter : IFuscObject
{
    bool Success { get; }
    bool Fail => !Success;
    dynamic ViewOutput { get; }
    IReadOnlyCollection<INotification> Notifications { get; }
   
    IOption<dynamic> Output { get; }

    bool HasExceptions { get; }

}
public interface IPresenter<TInput, TOutput> : IPresenter
{
    void OnErrorValidation(TInput input, IReadOnlyCollection<INotification> notifications);

    void OnError(List<Exception> exceptions, TInput input);

    void OnResult(TOutput output, IReadOnlyCollection<INotification> notifications, CancellationToken cancellationToken);

    new IOption<TOutput> Output { get; }
    void SetOutput(TOutput output);

  


}
