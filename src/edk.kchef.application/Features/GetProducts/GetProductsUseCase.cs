using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using edk.Kchef.Application.Fusc;
using edk.Kchef.Domain.Ordes;
using edk.Kchef.Domain.Products;
using MediatR;

namespace edk.Kchef.Application.Features.GetProducts
{
    public class GetProductsUseCase : UseCase<GetProductsRequest, GetProductsResponse>
    {
        protected override string NameUseCase => "GetProductsUseCase";

        public GetProductsUseCase(GetProductsPresenter presenter) : base(presenter)
        {

        }

        public override Task<GetProductsResponse> Handle(GetProductsRequest request, CancellationToken cancellationToken)
        {
            var products = new List<Product>()
            {
                new Product("Product 1", UnitType.Kg, 1),
                new Product("Product 2", UnitType.Litro, 10),
                new Product("Product 3", UnitType.Peca, 111)
            };

            return Task.FromResult(new GetProductsResponse(products, Notifications));
        }
    }
}