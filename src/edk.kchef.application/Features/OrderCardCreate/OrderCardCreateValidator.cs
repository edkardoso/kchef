using edk.Kchef.Domain.Common;
using FluentValidation;

namespace edk.Kchef.Application.Features.OrderCardCreate
{
    public class OrderCardCreateValidator : AbstractValidator<OrderCardCreateRequest>
    {
        public OrderCardCreateValidator()
        {
            RuleFor(r => r.InternalDeskCode)
                .NotNull()
                .NotEmpty()
                .WithMessage("É obrigatório ter o código da mesa.")
                .WithSeverity(Severity.Error);

            RuleFor(r => r.InternalDeskCode)
               .MaximumLength(SizeFields.SIZE_1)
               .WithMessage($"O código da mesa pode ter no máximo {SizeFields.SIZE_1} caracteres.")
               .WithSeverity(Severity.Error);

        }


    }


}
