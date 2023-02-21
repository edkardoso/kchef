using edk.Fusc.Contracts;
using edk.Fusc.Core.Validators;
using edk.Tools;

namespace edk.Fusc.Core.Presenters
{
    public abstract class PresenterBase<TInput, TOutput> : IPresenter<TInput, TOutput>
    {
        protected PresenterBase()
        {
            Output = new Option<TOutput>();
            ViewOutput = new Option<TOutput>();
            Notifications = new List<Notification>();
        }

        public bool Success => Notifications.HasError().Not();

        public bool HasExceptions => Notifications.HasException();

        public dynamic ViewOutput { get; protected set; }

        public IOption<TOutput> Output { get; protected set; }

        public IReadOnlyCollection<INotification> Notifications { get; protected set; }

        IOption<dynamic> IPresenter.Output => Option<dynamic>.New(Output.Match(o => o, () => default(dynamic)));

        public virtual void OnErrorValidation(TInput input, IReadOnlyCollection<INotification> notifications) {

            Notifications = notifications;
        
        }

        public virtual void OnError(List<Exception> exceptions, TInput input) { 
        
            
        }

        public virtual void OnResult(TOutput output, IReadOnlyCollection<INotification> notifications, CancellationToken cancellationToken) { }

        public void SetOutput(TOutput output) => Output = Option<TOutput>.New(output);



    }
}
