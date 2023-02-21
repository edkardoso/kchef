using edk.Kchef.Domain.Contracts.Repositories;
using edk.Kchef.Domain.Entities.Users;
using edk.Kchef.Infrastructure.Data.EF.Context;

namespace edk.Kchef.Infrastructure.Data.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(KChefContext dbContext) : base(dbContext)
    {
    }
}
