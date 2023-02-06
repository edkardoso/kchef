using System.Threading.Tasks;
using edk.Kchef.Domain.Common.Base;

namespace edk.Kchef.Domain.Contracts.Repositories;

public interface IUpdateGenericRepository<in TEntity> where TEntity : IEntity
{
    Task UpdateAsync(TEntity entity);
}
