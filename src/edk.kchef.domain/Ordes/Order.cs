using System;
using System.Collections.Generic;
using edk.Kchef.Domain.Common.Base;
using edk.Kchef.Domain.Users;

namespace edk.Kchef.Domain.Ordes
{

    public class Order : EntityBase
    {

        public Order(Waiter waiter, OrderCard card)
        {
            Waiter = waiter;
            Card = card;
            Status = OrderStatusType.Start;
        }
        public Waiter Waiter { get; protected set; }

        public ICollection<ItemOrder> Items { get; protected set; } = new List<ItemOrder>();

        public virtual OrderCard Card { get; protected set; }

        public DateTime Date { get; protected set; }
        public OrderStatusType Status { get; protected set; }

        public bool Canceled { get; protected set; }

        public void Add(ItemOrder item)
        {
            Items.Add(item);
        }

        public void Remove(ItemOrder item)
        {
            Items.Remove(item);
        }


    }
}
