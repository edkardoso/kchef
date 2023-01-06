using edk.Kchef.Domain.Common.Base;

namespace edk.Kchef.Domain.Ordes
{
    public class Product: EntityBase
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public UnitType Unity { get; private set; }
        public float AmountMin { get; private set; }
        public float AmountMax { get; private set; }
        public float Balance { get; private set; }
        public decimal Price { get; set; }

    }
}
