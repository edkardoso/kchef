using FluentValidation;

namespace edk.Kchef.Application.Features.OrderCardCreate
{
    public class OrderCardCreateValidator : AbstractValidator<OrderCardCreateRequest>
    {
        public OrderCardCreateValidator()
        {
            RuleFor(r=> r.InternalCodeDesk).NotEmpty();
        }
    }

   
}
