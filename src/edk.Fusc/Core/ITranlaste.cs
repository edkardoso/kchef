
using edk.Fusc.Core.Mediator;

namespace edk.Fusc.Core;

public interface ITranlaste
{
    Type Sender { get; }
    Type Receiver { get; }

    string FullName(dynamic obj) => UseCaseMediatorExtension.GetNameTranslate(Sender, Receiver, obj);
    TInput Convert<TInput>(dynamic obj);
}
