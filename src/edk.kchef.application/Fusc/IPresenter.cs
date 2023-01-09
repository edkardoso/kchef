using System;
using System.Collections.Generic;
using System.Threading;
using edk.Kchef.Domain.Common.Base;
using FluentValidation.Results;

namespace edk.Kchef.Application.Fusc
{

    public interface IPresenter<TInput, TOutput>
    {
        void OnError(TInput input, List<Notification> notifications);

        void OnException(TInput input, Exception exception);

        void OnSuccess(TOutput output, List<Notification> notifications, CancellationToken cancellationToken);

        TOutput Response { get; }

        bool Success { get; }

        dynamic ViewResponse { get; }
    }
}
