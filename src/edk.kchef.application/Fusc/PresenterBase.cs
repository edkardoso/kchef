using System;
using System.Collections.Generic;
using System.Threading;
using edk.Kchef.Domain.Common.Base;

namespace edk.Kchef.Application.Fusc
{
    public abstract class PresenterBase<TInput, TOutput> : IPresenter<TInput, TOutput>
    {
        public TOutput Response { get; protected set; }

        public bool Success { get; protected set; }

        public dynamic ViewResponse { get; protected set; }

        dynamic IPresenter.Response => Response;

        public virtual void OnError(TInput input, List<Notification> notifications)
        {
        }

        public virtual void OnException(TInput input, Exception exception)
        {
            throw exception;
        }

        public abstract void OnSuccess(TOutput output, List<Notification> notifications, CancellationToken cancellationToken);
       
    }
}
