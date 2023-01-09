using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using edk.Kchef.Domain.Common.Fusc;

namespace edk.Kchef.Application.Features.GetProducts
{
    public class ProductsRequest { }
    public class ProductsResponse { }
    public class GetProductsUseCase : UseCase<ProductsRequest, IEnumerable<ProductsResponse>>
    {
        public GetProductsUseCase()
        {

        }
        public override IEnumerable<ProductsResponse> OnExecute(ProductsRequest input)
        {
            throw new NotImplementedException();
        }
    }
}
