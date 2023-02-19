using System;
using edk.Kchef.Domain.Common.Base;
using edk.Kchef.Domain.Common.ValueObjects;

namespace edk.Kchef.Domain.Entities.Users
{

    public class User : EntityBase
    {
        public FullNameVO FullName { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateOnly ExpirationDate { get; set; }
        public bool Blocked { get; set; }
    }
}
