using System.Collections.Generic;
using edk.Fusc.Contracts;
using edk.Fusc.Core.Validators;
using edk.Kchef.Application.Common;
using FluentValidation;

namespace edk.Kchef.Application.Features.GetProducts;

public class GetProductsValidator : AbstractValidator<GetProductsRequest>, IUseCaseValidator<GetProductsRequest>
{
	public GetProductsValidator()
	{
		RuleFor(e => e.Id).NotEmpty().WithMessage("Errado");
	}

    IReadOnlyCollection<INotification> IUseCaseValidator<GetProductsRequest>.Validate(GetProductsRequest instance)
        => Validate(instance).Errors.ToNotifications();
}
