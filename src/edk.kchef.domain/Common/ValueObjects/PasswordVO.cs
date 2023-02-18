using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using edk.Kchef.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace edk.Kchef.Domain.Common.ValueObjects
{
    internal class PasswordVO : ValueObject
    {
        private PasswordHasher<User> _password;

        public string Value { get; }

        private PasswordVO(User user, string plainText)
        {
            _password = new PasswordHasher<User>();

            Value = _password.HashPassword(user, plainText);

        }


        public bool CheckForce() { return Value != null; }
    }
}
