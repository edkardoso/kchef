using System.Collections.Generic;
using edk.Fusc.Core.Outputs;
using edk.Kchef.Domain.Products;

namespace edk.Kchef.Application.Features.GetProducts;

public class GetProductsResponse : OutputPageBase
{
    public GetProductsResponse(List<Product> products, List<Fusc.Core.Validators.Notification> messages)
        : base(messages, products.Count, 1, 1)
    {
        Products = products;
    }

    public List<Product> Products { get; set; }

}