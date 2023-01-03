using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace edk.Kchef.Domain.Common.Base
{
    public abstract class EntityBase
    {
        public Guid Id { get; private set; }
        public bool Deleted { get; private set; }

        protected EntityBase()
        {
            if(Id == Guid.Empty)
            {
                Id = Guid.NewGuid();
            }
        }


    }
}
