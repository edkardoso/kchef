using edk.Fusc.Contracts;
using edk.Kchef.Application.Features.OrderCreate;
using Microsoft.AspNetCore.Mvc;

namespace edk.Kchef.Api.Controllers;

public class OrderController : BaseController
{
    public OrderController(IMediatorUseCase mediator)
        : base(mediator)
    {
    }

    [HttpPost(Name = "Create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult> CreateOrderAsync([FromBody] OrderCreateRequest request)
    {
        var presenter = await _mediator.HandleAsync<OrderCreateUseCase>(request);

        return presenter.ViewOutput;

    }
}
