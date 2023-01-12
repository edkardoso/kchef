using System;
using System.Collections.Generic;
using System.Threading;
using edk.Kchef.Domain.Common.Base;

namespace edk.Kchef.Application.Fusc.Presenters
{
    public interface IPresenter
    {
        bool Success { get; }
        dynamic ViewResponse { get; }
        dynamic Response { get; }
    }
    public interface IPresenter<TInput, TOutput> : IPresenter
    {
        void OnError(TInput input, List<Notification> notifications);

        void OnException(TInput input, Exception exception);

        void OnSuccess(TOutput output, List<Notification> notifications, CancellationToken cancellationToken);

        new TOutput Response { get; }


    }
}
