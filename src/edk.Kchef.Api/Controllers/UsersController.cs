using edk.Fusc.Contracts;
using edk.Kchef.Application.Features.GetProducts;
using edk.Kchef.Application.Features.Users.Create;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace edk.Kchef.Api.Controllers
{
    public class UsersController : BaseController
    {

        public UsersController(IMediatorUseCase mediator) : base(mediator)
        {
        }

        [HttpPost(Name = "CreateUser")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post([FromBody] CreateUserInput input)
        {
            
            var presenter = await _mediator.HandleAsync<CreateUserUseCase>(input);
            
            var result = presenter.Output.GetValueOrDefault(new UserOutput(null, "Nenhum", "Nenhum"));

            if (presenter.Success)
            {
                return new ObjectResult(result) { StatusCode = StatusCodes.Status201Created };

            }
            else if(presenter.HasExceptions)
            {
                return new ObjectResult(presenter.Notifications) { StatusCode = StatusCodes.Status500InternalServerError };
            }
            else
            {
                return BadRequest(presenter.Notifications);

            }



        }
    }
}
