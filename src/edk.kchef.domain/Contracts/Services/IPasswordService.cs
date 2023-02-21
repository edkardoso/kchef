using edk.Kchef.Domain.Entities.Users;
using System.Diagnostics.CodeAnalysis;

namespace edk.Kchef.Domain.Contracts.Services
{
    public interface IPasswordService
    {
        bool CheckForce([NotNull] string passwordPlainText);
        string GenerateHash([NotNull] User user, [NotNull] string passwordPlainText);
        bool VerifyPassword([NotNull] User user, [NotNull] string passwordPlainText);
        string GenerateRandomPassword();
    }
}