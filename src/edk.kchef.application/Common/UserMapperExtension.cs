using edk.Kchef.Application.Features.Users.Create;
using edk.Kchef.Domain.Common.ValueObjects;
using edk.Kchef.Domain.Entities.Users;
using System;

namespace edk.Kchef.Application.Common;

public static class UserMapperExtension
{
    public static UserOutput ToOutput(this User user)
        => new(user.Id, user.Login, user.Name.FirstName, user.ExpirationDate);

    public static User ToDomain(this CreateUserInput input)
        => new()
        {
            Login = input.Login,
            Email = input.Email,
            Name = new FullNameVO(input.FirstName, String.Empty, string.Empty, GenderType.None),
            Blocked = false,
        }; // TODO: Adicionar o Validate da entidade



}
