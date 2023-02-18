using edk.Kchef.Application.Features.Users.Create;
using edk.Kchef.Domain.Users;

namespace edk.Kchef.Application.Common
{
    public static class MapperExtension
    {
        public static UserOutput ToOutput(this User user) => new()
        {
            Id = user.Id,
            Login= user.Login,
            FirstName= user.FirstName,
            ExpirationDate = user.ExpirationDate

        };
    }

}
