using edk.Fusc.Contracts;
using edk.Fusc.Contracts.Common;

namespace edk.Fusc.Core.Validators;

public class ValidadorNull<TInput> : IUseCaseValidator<TInput>, INullableObject
{
    internal ValidadorNull() { }

    public bool IsNull() => true;

    public IReadOnlyCollection<Notification> Validate()
    {
        throw new NotImplementedException();
    }

    IReadOnlyCollection<INotification> IUseCaseValidator<TInput>.Validate(TInput instance) 
        => new List<INotification>().AsReadOnly();
}
