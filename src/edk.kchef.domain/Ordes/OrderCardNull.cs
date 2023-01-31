using edk.Kchef.Domain.Common.Base;

namespace edk.Kchef.Domain.Ordes
{
    public class OrderCardNull : OrderCard
    {
        public OrderCardNull() : this(null)
        {

        }
        public OrderCardNull(Desk desk) : base(desk)
        {
        }

        public override bool IsNull => true;
    }
}
