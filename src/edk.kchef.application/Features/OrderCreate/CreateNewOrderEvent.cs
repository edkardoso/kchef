using System;
using System.Collections.Generic;
using System.Linq;
using edk.Fusc.Contracts;
using edk.Fusc.Core.Events;
using edk.Kchef.Domain.Ordes;

namespace edk.Kchef.Application.Features.OrderCreate
{
    public class CreateNewOrderEvent : UseCaseEventBase
    {
        public CreateNewOrderEvent(Order order, IUseCase sender, IMediatorUseCase mediator)
            :base(sender)
        {
            this.OrderCardId = order.Id;
            this.Items = order.Items.ToList();
        }
        public Guid OrderCardId { get; }
        public IReadOnlyCollection<ItemOrder> Items { get; }

       
    }
}
