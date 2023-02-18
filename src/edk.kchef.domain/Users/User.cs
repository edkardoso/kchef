using System;
using edk.Kchef.Domain.Common.Base;

namespace edk.Kchef.Domain.Users
{
    public class User: EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateOnly ExpirationDate { get; set; }
        public bool Blocked { get; set; }
    }
}
