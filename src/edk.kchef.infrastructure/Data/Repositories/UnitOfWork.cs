using edk.Kchef.Domain.Contracts.Repositories;
using edk.Kchef.Infrastructure.Data.EF.Context;
using System.Threading.Tasks;

namespace edk.Kchef.Infrastructure.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly KChefContext _dbContext;

        public UnitOfWork(KChefContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CommitAsync() => await _dbContext.SaveChangesAsync().ConfigureAwait(false) > 0;

        public async Task RollbackAsync() => await Task.CompletedTask.ConfigureAwait(false);

    }
}
