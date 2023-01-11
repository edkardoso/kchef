using edk.Kchef.Application.Features.GetProducts;
using edk.Kchef.Application.Fusc;
using Microsoft.AspNetCore.Mvc;

namespace edk.Kchef.Api.Controllers;

public class BaseController : ControllerBase
{
    protected readonly IMediatorUseCase _mediator;

    public BaseController(IMediatorUseCase mediator)
    {
        _mediator = mediator;
    }
}

[Route("api/[controller]")]
[ApiController]
public class ProductsController : BaseController
{


    public ProductsController(IMediatorUseCase mediator) 
        : base(mediator)
    {
    }

    [HttpGet(Name = "GetProducts")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetAsync([FromServices] GetProductsUseCase useCase,[FromQuery] GetProductsRequest request)
    {
        var presenter = await useCase.HandleAsync(request);

        return presenter.ViewResponse;

    }
}
