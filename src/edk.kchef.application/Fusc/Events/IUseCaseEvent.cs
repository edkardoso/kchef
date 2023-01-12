using System;

namespace edk.Kchef.Application.Fusc.Events;

public interface IUseCaseEvent
{
    Type Sender { get; }
}
