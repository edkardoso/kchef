using System;
using System.Collections.Generic;
using edk.Kchef.Domain.Common;
using edk.Kchef.Domain.Common.Base;
using edk.Kchef.Domain.Common.ValueObjects;
using edk.Kchef.Domain.Contracts.Repositories;

namespace edk.Kchef.Domain.Entities.Users
{

    public class User : EntityBase, IAggregateRoot
    {
        public FullNameVO Name { get; init; }
        public string Login { get; init; }
        public string Email { get; init; }
        public string Password { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public bool Blocked { get; init; }

        public void SetPassword(string password, DateTime currentDate)
        {
            if(Password != null)
            {
                throw new InvalidOperationException("Este usuário já possui uma senha.");
            }

            Password = password;
            ExpirationDate = currentDate.AddDays(Setup.PASSWORD_EXPIRATION_DAYS);
        }
    }
}
