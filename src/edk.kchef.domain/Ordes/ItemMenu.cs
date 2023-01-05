using System;

namespace edk.Kchef.Domain.Ordes
{
    public class ItemMenu {
        public ItemMenu(string code, string description, decimal price)
        {
            Code = code;
            Description = description;
            Price = price;
        }

        public string Code { get; set; }
        public string Description { get; set; }

        public Decimal Price { get; set; }
    }
}
