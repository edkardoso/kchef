using FluentValidation;
namespace edk.Kchef.Application.Features.GetProducts;

public class GetProductsValidator : AbstractValidator<GetProductsRequest>
{
	public GetProductsValidator()
	{
		RuleFor(e => e.Id).NotEmpty().WithMessage("Errado");
	}
}