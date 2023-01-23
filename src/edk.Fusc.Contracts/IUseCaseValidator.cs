using edk.Fusc.Contracts;

namespace edk.Fusc.Core.Validators;

public interface IUseCaseValidator<TInput> : IUseCaseValidator
{
    IReadOnlyCollection<INotification> Validate(TInput instance);
}

public interface IUseCaseValidator
{
    IReadOnlyCollection<INotification> Validate()=>  new List<INotification>();
}
