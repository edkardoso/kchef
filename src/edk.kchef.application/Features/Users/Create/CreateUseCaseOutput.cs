using System;

namespace edk.Kchef.Application.Features.Users.Create;

public record UserOutput(Guid Id, string Login, string FirstName, DateOnly ExpirationDate);
