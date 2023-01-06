using edk.Kchef.Domain.Common.Base;

namespace edk.Kchef.Domain.Ordes
{
    public class Product: EntityBase
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public UnitType Unidade { get; private set; }
        public decimal AmountMin { get; private set; }
        public decimal AmountMax { get; private set; }
        public decimal Balance { get; private set; }

    }
}
