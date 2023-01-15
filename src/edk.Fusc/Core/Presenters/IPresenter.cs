using edk.Fusc.Core.Validators;

namespace edk.Fusc.Core.Presenters
{
    public interface IPresenter
    {
        bool Success { get; }
        dynamic ViewOutput { get; }
        Option<dynamic> Output { get; }

        void SetSuccess(bool value);
    }
    public interface IPresenter<TInput, TOutput> : IPresenter
    {
        void OnError(TInput input, List<Notification> notifications);

        void OnException(Exception exception, TInput input);

        void OnSuccess(TOutput output, List<Notification> notifications, CancellationToken cancellationToken);

        new Option<TOutput> Output { get; }
        void SetOutput(TOutput output);


    }
}
