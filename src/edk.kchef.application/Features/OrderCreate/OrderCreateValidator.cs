using System;
using FluentValidation;

namespace edk.Kchef.Application.Features.OrderCreate
{
    public class OrderCreateValidator : AbstractValidator<OrderCreateRequest>
    {
        public OrderCreateValidator()
        {

            RuleFor(r => r.DeskInternalCode).NotEmpty()
                .When(r => r.OrderCard == Guid.Empty)
                .WithMessage("A identificação da mesa é necessária.");


            RuleFor(r => r.OrderCard).NotEmpty()
               .When(r => string.IsNullOrWhiteSpace(r.DeskInternalCode))
               .WithMessage("A identificação da comanda é necessária.");


        }
    }
}
