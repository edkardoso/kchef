using System;

namespace edk.Kchef.Application.Features.Users.Create
{
    public class UserOutput
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string FirstName { get; set; }
        public DateOnly ExpirationDate { get; set; }
    }
}