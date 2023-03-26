using System.Threading.Tasks;
using edk.Kchef.Domain.Ordes;
using edk.Tools.Common;

namespace edk.Kchef.Domain.Contracts.Repositories;

public interface IDeskRepository: IReadGenericRepository<Desk>, IUpdateGenericRepository<Desk>
{
    Task<Option<Desk>> SingleByCodeAsync(string code);

}
