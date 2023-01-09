using System;
using System.Collections.Generic;
using System.Threading;
using edk.Kchef.Domain.Common.Base;
using FluentValidation.Results;

namespace edk.Kchef.Application.Fusc
{
    public class PresenterNull<TInput, TOutput> : IPresenter<TInput, TOutput>
    {
        public TOutput Response { get; private set; }

        public dynamic ViewResponse => Response;

        public bool Success { get; private set; }

        public void OnError(TInput input, List<Notification> notifications)
        {
            return;
        }

        public void OnException(TInput input, Exception exception)
        {
            return;
        }

        public void OnSuccess(TOutput output, List<Notification> notifications, CancellationToken cancellationToken)
        {
            Response = output;
            Success = true;
        }
    }
}
