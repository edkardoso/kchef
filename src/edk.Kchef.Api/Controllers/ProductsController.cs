using edk.Fusc.Contracts;
using edk.Kchef.Application.Features.GetProducts;
using Microsoft.AspNetCore.Mvc;

namespace edk.Kchef.Api.Controllers;


public class ProductsController : BaseController
{
 
    public ProductsController(IMediatorUseCase mediator)
        : base(mediator)
    {
    }

    //[HttpGet(Name = "GetProducts")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<ActionResult> GetAsync([FromQuery] GetProductsRequest request)
    //{
    //    //var presenter = await _mediator.HandleAsync<GetProductsUseCase>(request);

    //    //return presenter.ViewOutput;

    //}
}
