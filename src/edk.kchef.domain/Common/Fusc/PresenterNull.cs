using System;
using System.Collections.Generic;
using edk.Kchef.Domain.Common.Base;
using FluentValidation.Results;

namespace edk.Kchef.Domain.Common.Fusc
{
    public class PresenterNull<TInput, TOutput> : IPresenter<TInput, TOutput>
    {
        public TOutput Result { get; private set; }

        public dynamic ViewResult => Result;

        public bool Success { get; private set; }

        public void OnError(TInput input, List<Notification> notifications)
        {
            return;
        }

        public void OnException(TInput input, Exception exception)
        {
            return;
        }

        public void OnSuccess(TOutput output, List<Notification> notifications)
        {
            Result = output;
            Success = true;
        }
    }
}
