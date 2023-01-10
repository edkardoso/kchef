using MediatR;
namespace edk.Kchef.Application.Features.GetProducts
{
    public class GetProductsRequest : IRequest<GetProductsResponse> {
        public int Id { get; set; }

    }
}