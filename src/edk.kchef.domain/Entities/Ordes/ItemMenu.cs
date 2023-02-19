using System;
using System.Collections.Generic;
using edk.Kchef.Domain.Common.Base;
using edk.Kchef.Domain.Products;

namespace edk.Kchef.Domain.Ordes
{
    public class ItemMenu: EntityBase {
        public ItemMenu(string code, string description, decimal price):base()
        {
            Code = code;
            Description = description;
            Price = price;
        }

        public string Code { get; set; }
        public string Description { get; set; }
        public Decimal Price { get; set; }
        public List<Product>  Products { get; set; }
    }
}
