using System;

namespace edk.Kchef.Application.Features.Users.Create;

#nullable enable
public record UserOutput(Guid? Id = null, string? Login = null, string? FirstName = null, DateTime? ExpirationDate = null);
