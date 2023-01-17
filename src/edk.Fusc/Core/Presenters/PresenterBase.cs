using edk.Fusc.Core.Outputs;
using edk.Fusc.Core.Validators;

namespace edk.Fusc.Core.Presenters
{
    public abstract class PresenterBase<TInput, TOutput> : IPresenter<TInput, TOutput>
    {

        public bool Success { get; protected set; }

        public dynamic ViewOutput { get; protected set; }

        public Option<TOutput> Output { get; protected set; }

        Option<dynamic> IPresenter.Output => Option<dynamic>.New(Output.Value);

        public virtual void OnErrorValidation(TInput input, IReadOnlyCollection<Notification> notifications) { }

        public virtual void OnError(Exception exception, TInput input) { }

        public virtual void OnResult(TOutput output, IReadOnlyCollection<Notification> notifications, CancellationToken cancellationToken) { }

        public void SetSuccess(bool value) => Success = value;

        public void SetOutput(TOutput output) => Output = Option<TOutput>.New(output);
    }
}
