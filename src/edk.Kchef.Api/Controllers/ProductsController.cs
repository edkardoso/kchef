using edk.Kchef.Application.Features.GetProducts;
using Microsoft.AspNetCore.Mvc;

namespace edk.Kchef.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{

    private readonly GetProductsUseCase _useCase;
    public ProductsController()
    {
        _useCase = new GetProductsUseCase(new GetProductsPresenter());
    }

    [HttpGet(Name = "GetProducts")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetAsync([FromQuery] GetProductsRequest request)
    {
        var result = await _useCase.HandleAsync(request);

        var response = result.Presenter.ViewResponse;

        return response;

    }
}
