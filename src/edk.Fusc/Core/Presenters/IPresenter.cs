using edk.Fusc.Core.Outputs;
using edk.Fusc.Core.Validators;

namespace edk.Fusc.Core.Presenters
{
    public interface IPresenter
    {
        bool Success { get;  }
        dynamic ViewOutput { get; }
        Option<dynamic> Output { get; }

    }
    public interface IPresenter<TInput, TOutput> : IPresenter
    {
        void OnErrorValidation(TInput input, IReadOnlyCollection<Notification> notifications);

        void OnError(Exception exception, TInput input);

        void OnResult(TOutput output, IReadOnlyCollection<Notification> notifications, CancellationToken cancellationToken);

        new Option<TOutput> Output { get; }
        void SetOutput(TOutput output);

        void SetSuccess(bool success);


    }
}
