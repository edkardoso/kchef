using edk.Fusc.Core.Validators;

namespace edk.Fusc.Core.Presenters
{
    public abstract class PresenterBase<TInput, TOutput> : IPresenter<TInput, TOutput>
    {
        public TOutput Output { get; protected set; }

        public bool Success { get; protected set; }

        public dynamic ViewOutput{ get; protected set; }

        dynamic IPresenter.Output => Output;

        public virtual void OnError(TInput input, List<Notification> notifications)
        {
        }

        public virtual void OnException(Exception exception, TInput input)
        {

        }

        public abstract void OnSuccess(TOutput output, List<Notification> notifications, CancellationToken cancellationToken);

        public void SetSuccess(bool value) => Success = value;
    }
}
