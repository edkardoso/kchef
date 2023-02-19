using edk.Kchef.Application.Features.Users.Create;
using edk.Kchef.Domain.Entities.Users;

namespace edk.Kchef.Application.Common;

public static class MapperExtension
{
    public static UserOutput ToOutput(this User user)
        => new(user.Id, user.Login, user.FullName.FirstName, user.ExpirationDate);

   
}
