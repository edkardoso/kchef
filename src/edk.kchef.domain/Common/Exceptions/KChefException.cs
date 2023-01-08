using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using edk.Kchef.Domain.Common.Base;

namespace edk.Kchef.Domain.Common.Exceptions
{
    public abstract class KChefException : Exception
    {
        public IReadOnlyList<Notification> Notifications { get; }

        protected KChefException()
        {
        }

        protected KChefException(IReadOnlyList<Notification> notifications)
        {
            Notifications = notifications;
        }

        protected KChefException(string message) : base(message)
        {
        }

        protected KChefException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected KChefException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

       
    }
}
