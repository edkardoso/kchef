using System;
using edk.Kchef.Domain.Common.Base;

namespace edk.Kchef.Domain.Ordes
{
    public class ItemOrder : EntityBase<ItemOrder>
    {
        public ItemMenu Item { get; private set; }
        public int Amount { get; private set; }
        public Decimal Price { get; private set; }

        public bool Free { get; private set; }

        public ItemOrder(ItemMenu item, int amount=1)
        {
            Item = item;
            Amount = amount;
            Price = item.Price;
            Free = false;
        }

        public void SetFree(string justification)
        {
            if (String.IsNullOrWhiteSpace(justification))
            {
                throw new InvalidOperationException("Uma justificativa é obrigatória para colocar um item como cortesia.");
            }

            Free = true;
        }
    }
}
