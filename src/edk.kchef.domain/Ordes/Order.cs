using System.Linq;
using System.Text;
using System.Threading.Tasks;
using edk.Kchef.Domain.Common.Base;
using edk.Kchef.Domain.Users;

namespace edk.Kchef.Domain.Ordes
{

    public class Order: EntityBase
    {
        public Waiter Waiter { get; protected set; }
    }
}
