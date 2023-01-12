using System;

namespace edk.Kchef.Application.Fusc;

public interface ITranlaste
{
    Type Sender { get; }
    Type Receiver { get; }

    string FullName(dynamic obj) => UseCaseMediatorExtension.GetNameTranslate(Sender, Receiver, obj);
    TInput Convert<TInput>(dynamic obj);
}
