using System;
using System.Collections.Generic;
using System.Linq;
using edk.Kchef.Domain.Common.Base;
using edk.Kchef.Domain.Entities.Users;

namespace edk.Kchef.Domain.Ordes
{

    public class Order : EntityBase
    {

        public Order(Waiter waiter)
        {
            Waiter = waiter;
            Status = OrderStatusType.Start;
        }
        public Waiter Waiter { get; protected set; }

        public ICollection<ItemOrder> Items { get; protected set; } = new List<ItemOrder>();

        public virtual OrderCard Card { get; protected set; }

        public DateTime Date { get; protected set; }
        public OrderStatusType Status { get; protected set; }

        public bool Canceled { get; protected set; }

        public void AddRange(IEnumerable<ItemOrder> items)
        {
            items.ToList().ForEach(item => Items.Add(item));
        }

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
