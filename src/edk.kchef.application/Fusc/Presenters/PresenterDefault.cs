using System.Collections.Generic;
using System.Threading;
using edk.Kchef.Domain.Common.Base;

namespace edk.Kchef.Application.Fusc.Presenters
{
    public partial class PresenterDefault<TInput, TOutput> : PresenterBase<TInput, TOutput>
    {
        public PresenterDefault()
        {

        }
        public PresenterDefault(TOutput output)
        {
            Response = output;
        }

        public override void OnSuccess(TOutput output, List<Notification> notifications, CancellationToken cancellationToken)
        {
            Response = output;
            Success = true;
        }
    }
}
