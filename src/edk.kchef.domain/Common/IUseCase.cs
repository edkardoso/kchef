﻿namespace edk.Kchef.Domain.Common
{
    public interface IUseCase { 
    }
    public interface IUseCase<TInput, TOutput> : IUseCase
    {
        TOutput Result { get; }
       
        void Handler(IUseCase<TInput, TOutput> other);
    }
}
