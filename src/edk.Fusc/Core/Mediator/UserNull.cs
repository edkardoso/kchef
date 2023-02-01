using edk.Fusc.Contracts;

namespace edk.Fusc.Core.Mediator;

public class UserNull : IUser
{
    internal UserNull()
    {

    }

    public string Name => "Unknown";

    public string Id => "00000";

    public string Email => "none@email.com";

    public string CompanyId => "00000";
}