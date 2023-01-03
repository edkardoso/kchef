using edk.Kchef.Domain.Common.Base;

namespace edk.Kchef.Domain.Ordes
{
    public class OrderCard : EntityBase
    {
        public virtual Desk Desk { get; protected set; }
    }
}
