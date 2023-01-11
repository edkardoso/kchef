using System;

namespace edk.Kchef.Application.Fusc;

//public interface IMediator
//{
//    UseCaseMediator Register<TUseCase, TUseCaseImpl, TInput, TOutput, TValidator, TValidatorImpl, TPresenter, TPresenterImpl>()
//        where TUseCase : class, IUseCase<TInput, TOutput>
//        where TUseCaseImpl : class, TUseCase
//        where TValidator : class
//        where TValidatorImpl : class, TValidator
//        where TPresenter : class
//        where TPresenterImpl : class, TPresenter;

//    Task<IPresenter<TInput, TOutput>> Send<TService, TInput, TOutput>(TInput request)
//        where TInput : IRequest<TOutput>
//        where TService : IUseCase<TInput, TOutput>;
//}

public interface ITranlaste
{
    Type Sender { get; }
    Type Receiver { get; }

    string FullName(dynamic obj) => UseCaseMediatorExtension.GetNameTranslate(Sender, Receiver, obj);
    TInput Convert<TInput>(dynamic obj);
}
