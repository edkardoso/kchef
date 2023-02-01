using edk.Fusc.Contracts;
using edk.Tools;

namespace edk.Fusc.Core.Presenters
{
    public abstract class PresenterBase<TInput, TOutput> : IPresenter<TInput, TOutput>
    {
        protected PresenterBase()
        {
            Output = new Option<TOutput>();
            ViewOutput = new Option<TOutput>();
        }

        public bool Success { get; protected set; }

        public dynamic ViewOutput { get; protected set; }

        public IOption<TOutput> Output { get; protected set; }

        IOption<dynamic> IPresenter.Output => Option<dynamic>.New(Output.Match(o => o, () => default(dynamic)));

        public virtual void OnErrorValidation(TInput input, IReadOnlyCollection<INotification> notifications) { }

        public virtual void OnError(Exception exception, TInput input) { }

        public virtual void OnResult(TOutput output, IReadOnlyCollection<INotification> notifications, CancellationToken cancellationToken) { }

        public void SetSuccess(bool value) => Success = value;

        public void SetOutput(TOutput output) => Output = Option<TOutput>.New(output);


    }
}
