using edk.Kchef.Application.Features.GetProducts;
using Microsoft.AspNetCore.Mvc;

namespace edk.Kchef.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    [HttpGet(Name = "GetProducts")]
    public IEnumerable<ProductsResponse> Get([FromServices] GetProductsUseCase useCase, ProductsRequest request)
    {
        return useCase.HandleAsync(request).Result.Presenter.Response;

    }
}
