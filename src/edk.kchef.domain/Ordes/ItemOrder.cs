using System;
using edk.Kchef.Domain.Common.Base;

namespace edk.Kchef.Domain.Ordes
{
    public class ItemOrder: EntityBase
    {
        public ItemMenu Item { get; set; }
        public int Amount { get; set; }
        public Decimal Price { get; set; }

        public bool Free { get; set; }
    }
}
