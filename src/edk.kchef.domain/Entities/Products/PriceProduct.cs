using System;
using edk.Kchef.Domain.Common.Base;

namespace edk.Kchef.Domain.Products;

public class ProductPrice : EntityBase
{
    public DateTime Date { get; private set; }
    public decimal Price { get; private set; }
    public override bool Deleted => false;
    public virtual Product Product { get; private set; }

}
