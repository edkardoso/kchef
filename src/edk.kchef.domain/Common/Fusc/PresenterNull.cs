using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace edk.Kchef.Domain.Common.Fusc
{
    public class PresenterNull<TInput, TOutput> : IPresenter<TInput, TOutput>
    {
        public TOutput Result { get; private set; }

        public dynamic ViewResult => Result;

        public bool Success { get; private set; }

        public void OnError(TInput input, List<ValidationFailure> errors)
        {
            return;
        }

        public void OnException(TInput input, Exception exception)
        {
            return;
        }

        public void OnSuccess(TOutput output)
        {
            Result = output;
            Success = true;
        }
    }
}
