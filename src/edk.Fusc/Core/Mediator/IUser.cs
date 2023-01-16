namespace edk.Fusc.Core.Mediator;

public interface IUser
{
    string Name { get; }
    public string Id { get;  }
    public string Email { get;  }
    public string CompanyId { get;  }
}

public class UserNull : IUser
{
    public string Name => "Unknown";

    public string Id => "00000";

    public string Email => "none@email.com";

    public string CompanyId => "00000";
}