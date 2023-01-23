using edk.Fusc.Contracts;
using edk.Fusc.Core.Outputs;

namespace edk.Fusc.Core.Presenters
{
    public partial class PresenterDefault<TInput, TOutput> : PresenterBase<TInput, TOutput>
    {
        public PresenterDefault(){}
        public PresenterDefault(TOutput output)
        {
            Output = Option<TOutput>.New(output);
        }

        public override void OnResult(TOutput output, IReadOnlyCollection<INotification> notifications, CancellationToken cancellationToken)
        {}
    }
}
