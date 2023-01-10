using System.Collections.Generic;
using edk.Kchef.Domain.Common.Base;

namespace edk.Kchef.Application.Fusc
{
    public interface IOutput
    {
        List<Notification> Messages { get; set; }
    }
}
