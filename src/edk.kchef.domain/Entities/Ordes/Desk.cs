using System.Collections.Generic;
using edk.Kchef.Domain.Common.Base;

namespace edk.Kchef.Domain.Ordes
{
    public class Desk: EntityBase
    {
        public Desk(string internalCode)
        {
            InternalCode = internalCode;
        }
        public string InternalCode { get; protected set; }
        public ICollection<OrderCard> Cards { get; protected set; }

        public bool Available { get;  }
    }
}
