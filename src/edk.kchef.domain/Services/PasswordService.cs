using edk.Kchef.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace edk.Kchef.Domain.Services;

internal class PasswordService
{
    private readonly OptionsPassword _options;
    private readonly PasswordRule _rules;

    public PasswordService(OptionsPassword options = null)
    {
        _options = options ?? new OptionsPassword();
        _rules = new(options);

    }

    public bool CheckForce(string passwordPlainText)
        => passwordPlainText != null
            && _rules.MinLength(passwordPlainText)
            && _rules.MinLength(passwordPlainText)
            && _rules.HasLowerCharacter(passwordPlainText)
            && _rules.HasUpperCharacter(passwordPlainText)
            && _rules.HasDigit(passwordPlainText)
            && _rules.HasSpecialCharacter(passwordPlainText);
 

    public string GenerateHash(User user, string passwordPlainText)
    {
        var _passwordHasher = new PasswordHasher<User>();

        return _passwordHasher.HashPassword(user, passwordPlainText);
    }

    public bool VerifyPassword(User user, string passwordPlainText)
    {
        var password = GenerateHash(user, passwordPlainText);

        return password.Equals(user.Password);

    }

}
