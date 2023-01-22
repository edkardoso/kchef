namespace edk.Fusc.Core.Validators;

public class ValidadorNull<TInput> : IUseCaseValidator<TInput>
{
    public ValidadorNull() { }

    public IReadOnlyCollection<Notification> Validate()
    {
        throw new NotImplementedException();
    }

    IReadOnlyCollection<Notification> IUseCaseValidator<TInput>.Validate(TInput instance) 
        => new List<Notification>().AsReadOnly();
}
