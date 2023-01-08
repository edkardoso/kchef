using System;
using System.Collections.Generic;
using edk.Kchef.Domain.Common.Base;
using FluentValidation.Results;

namespace edk.Kchef.Domain.Common.Fusc
{

    public interface IPresenter<TInput, TOutput>
    {
        public abstract void OnError(TInput input, List<Notification> notifications);

        public abstract void OnException(TInput input, Exception exception);

        public abstract void OnSuccess(TOutput output, List<Notification> notifications);

        public TOutput Result { get; }

        public bool Success { get; }

        public dynamic ViewResult { get; }
    }
}
