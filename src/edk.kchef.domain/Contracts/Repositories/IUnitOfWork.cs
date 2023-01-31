using System.Threading.Tasks;

namespace edk.Kchef.Domain.Contracts.Repositories;

public interface IUnitOfWork
{
    Task<bool> CommitAsync();
    Task RollbackAsync();
}