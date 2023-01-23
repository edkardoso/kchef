namespace edk.Fusc.Contracts;

public interface IUser
{
    string Name { get; }
    public string Id { get; }
    public string Email { get; }
    public string CompanyId { get; }
}
