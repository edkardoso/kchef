using System.Collections.Generic;
using edk.Kchef.Domain.Common.Base;

namespace edk.Kchef.Domain.Common.Exceptions
{
    public class InvalidEntityException : KChefException
    {
        public InvalidEntityException(IReadOnlyList<Notification> notifications) : base(notifications)
        {
        }
    }
}
