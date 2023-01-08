using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace edk.Kchef.Domain.Common.Fusc
{

    public interface IPresenter<TInput, TOutput>
    {
        public abstract void OnError(TInput input, List<ValidationFailure> errors);

        public abstract void OnException(TInput input, Exception exception);

        public abstract void OnSuccess(TOutput output);

        public TOutput Result { get; }

        public bool Success { get; }

        public dynamic ViewResult { get; }
    }
}
