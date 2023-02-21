using edk.Kchef.Domain.Contracts.Services;
using edk.Kchef.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;

namespace edk.Kchef.Domain.Services;

public class PasswordService : IPasswordService
{
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly OptionsPasswordRule _options;
    private readonly PasswordRule _rules;

    public PasswordService(IPasswordHasher<User> passwordHasher, OptionsPasswordRule options = null)
    {
        _passwordHasher = passwordHasher ?? throw new System.ArgumentNullException(nameof(passwordHasher));
        _options = options ?? new();
        _rules = new(options);

    }

    public bool CheckForce([NotNull] string passwordPlainText)
        => _rules.MinLength(passwordPlainText)
            && _rules.MaxLength(passwordPlainText)
            && _rules.HasLowerCharacter(passwordPlainText)
            && _rules.HasUpperCharacter(passwordPlainText)
            && _rules.HasDigit(passwordPlainText)
            && _rules.HasSpecialCharacter(passwordPlainText);

    public string GenerateHash([NotNull] User user, [NotNull] string passwordPlainText)
        => _passwordHasher.HashPassword(user, passwordPlainText);

    public string GenerateRandomPassword()
    {
        throw new System.NotImplementedException();
    }

    public bool VerifyPassword([NotNull] User user, [NotNull] string passwordPlainText)
        => _passwordHasher.VerifyHashedPassword(user, user.Password, passwordPlainText) != PasswordVerificationResult.Failed;

}
