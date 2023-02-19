using System.Threading.Tasks;
using edk.Tools;
using edk.Kchef.Domain.Ordes;

namespace edk.Kchef.Domain.Contracts.Repositories;

public interface IDeskRepository: IReadGenericRepository<Desk>, IUpdateGenericRepository<Desk>
{
    Task<Option<Desk>> SingleByCodeAsync(string code);

}
