using edk.Fusc.Contracts;
using edk.Fusc.Core.Outputs;
using edk.Tools;

namespace edk.Fusc.Core.Presenters
{
    public partial class PresenterDefault<TInput, TOutput> : PresenterBase<TInput, TOutput>
    {
        public PresenterDefault(){}
        internal PresenterDefault(TOutput output)
        {
            Output = Option<TOutput>.New(output);
        }

        public override void OnResult(TOutput output, IReadOnlyCollection<INotification> notifications, CancellationToken cancellationToken)
        {}
    }
}
