namespace edk.Fusc.Core.Validators;

public interface IUseCaseValidator<in T> : IUseCaseValidator
{
    IReadOnlyCollection<Notification> Validate(T instance);
}

public interface IUseCaseValidator
{
    IReadOnlyCollection<Notification> Validate()=>  new List<Notification>();
}
