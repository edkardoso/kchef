using System;
using System.Collections.Generic;
using System.Threading;
using edk.Kchef.Domain.Common.Base;

namespace edk.Kchef.Application.Fusc
{

    public abstract class PresenterBase : IPresenter<object, object>
    {
        public object Response => throw new NotImplementedException();

        public bool Success => throw new NotImplementedException();

        public dynamic ViewResponse => throw new NotImplementedException();

        public void OnError(object input, List<Notification> notifications)
        {
            throw new NotImplementedException();
        }

        public void OnException(object input, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void OnSuccess(object output, List<Notification> notifications, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    public interface IPresenter {
        bool Success { get; }
        dynamic ViewResponse { get; }
    }
    public interface IPresenter<TInput, TOutput> : IPresenter
    {
        void OnError(TInput input, List<Notification> notifications);

        void OnException(TInput input, Exception exception);

        void OnSuccess(TOutput output, List<Notification> notifications, CancellationToken cancellationToken);

        TOutput Response { get; }

      
    }
}
