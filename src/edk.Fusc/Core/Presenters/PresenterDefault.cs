namespace edk.Fusc.Core.Presenters
{
    public partial class PresenterDefault<TInput, TOutput> : PresenterBase<TInput, TOutput>
    {
        public PresenterDefault()
        {

        }
        public PresenterDefault(TOutput output)
        {
            Output = output;
        }

        public override void OnSuccess(TOutput output, List<Validators.Notification> notifications, CancellationToken cancellationToken)
        {
            Output = output;
            Success = true;
        }
    }
}
