using System.Collections.Generic;
using edk.Kchef.Domain.Common.Base;
using edk.Kchef.Domain.Ordes;

namespace edk.Kchef.Domain.Products;

public class Product : EntityBase<Product>
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public UnitType Unity { get; private set; }
    public float AmountMin { get; private set; }
    public float AmountMax { get; private set; }
    public float Balance { get; private set; }
    public ICollection<ProductPrice> Prices { get; private set; }

}
