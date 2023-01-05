using FluentValidation;

namespace edk.Kchef.Domain.Common
{
    public class ValidadorNull<TInput> : AbstractValidator<TInput>
    {
        public ValidadorNull() { }
    }
}
