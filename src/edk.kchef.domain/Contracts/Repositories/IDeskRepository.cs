using System.Threading.Tasks;
using System;
using edk.Kchef.Domain.Ordes;
using edk.Tools;

namespace edk.Kchef.Domain.Contracts.Repositories;

public interface IDeskRepository: IReadGenericRepository<Desk>, IUpdateGenericRepository<Desk>
{
    Task<Option<Desk>> SingleByCodeAsync(string code);

}
