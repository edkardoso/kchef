using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using edk.Kchef.Application.Fusc;
using FluentValidation;
using MediatR;

namespace edk.Kchef.Application.Features.GetProducts
{
    public class ProductsRequest : IRequest<IEnumerable<ProductsResponse>> { }
    public class ProductsResponse { }
    public class GetProductsUseCase : UseCase<ProductsRequest, IEnumerable<ProductsResponse>>
    {
        protected override string NameUseCase => "GetProductsUseCase";

        public override Task<IEnumerable<ProductsResponse>> Handle(ProductsRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }


}
