using edk.Fusc.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace edk.Kchef.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    protected readonly IMediatorUseCase _mediator;

    protected BaseController(IMediatorUseCase mediator)
    {
        _mediator = mediator;
    }
}
