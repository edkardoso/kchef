using System.Collections.Generic;
using edk.Kchef.Domain.Common.Base;

namespace edk.Kchef.Domain.Ordes
{
    public class Desk: EntityBase
    {
        public string Code { get; set; }
        public ICollection<OrderCard> Cards { get; protected set; }
    }
}
