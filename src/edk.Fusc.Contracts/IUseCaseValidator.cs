using edk.Fusc.Contracts;
using edk.Fusc.Contracts.Common;

namespace edk.Fusc.Core.Validators;

public interface IUseCaseValidator<TInput> : IUseCaseValidator
{
    IReadOnlyCollection<INotification> Validate(TInput? input);
}

public interface IUseCaseValidator : IFuscObject, INullableObject
{
    IReadOnlyCollection<INotification> Validate()=>  new List<INotification>();
}


