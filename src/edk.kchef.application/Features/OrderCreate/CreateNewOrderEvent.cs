using System;
using System.Collections.Generic;
using System.Linq;
using edk.Kchef.Application.Fusc.Events;
using edk.Kchef.Domain.Ordes;

namespace edk.Kchef.Application.Features.OrderCreate
{
    public class CreateNewOrderEvent : IUseCaseEvent
    {
        public CreateNewOrderEvent(Order order, Type sender)
        {
            this.OrderCardId = order.Id;
            this.Items = order.Items.ToList();
            Sender = sender;
        }
        public Guid OrderCardId { get; }
        public IReadOnlyCollection<ItemOrder> Items { get; }

        public Type Sender { get; }
    }
}
