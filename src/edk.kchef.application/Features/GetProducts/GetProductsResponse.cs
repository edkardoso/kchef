using System.Collections.Generic;
using edk.Kchef.Application.Fusc;
using edk.Kchef.Domain.Common.Base;
using edk.Kchef.Domain.Ordes;
using edk.Kchef.Domain.Products;

namespace edk.Kchef.Application.Features.GetProducts
{
    public class GetProductsResponse : OutputPage
    {
        public GetProductsResponse(List<Product> products, List<Notification> messages)
            : base(messages, products.Count, 1, 1)
        {
            Products = products;
        }

        public List<Product> Products { get; set; }

    }
}