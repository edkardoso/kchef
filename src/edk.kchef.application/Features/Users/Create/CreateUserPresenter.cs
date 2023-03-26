using edk.Fusc.Contracts;
using edk.Fusc.Core.Presenters;
using edk.Kchef.Application.Common;
using edk.Kchef.Domain.Common.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;

namespace edk.Kchef.Application.Features.Users.Create;

public class CreateUserPresenter : PresenterBase<CreateUserInput, UserOutput>
{
    public override void OnResult(UserOutput output, IReadOnlyCollection<INotification> notifications, CancellationToken cancellationToken)
    {
        var result = new ResultApi(output, notifications.ToStringList());

        result.AddLink(HateoasContants.SELF, $"https://localhost:7005/api/Users/{output.Id}");

        SetViewOutput(new ObjectResult(result) { StatusCode = StatusCodes.Status201Created });
    }

    public override void OnErrorValidation(CreateUserInput input, IReadOnlyCollection<INotification> notifications)
    {
        var newInput = new CreateUserInput(input.Login, input.Email, input.FirstName, "********");

        var result = new ResultApi(newInput, notifications.ToStringList());

        SetViewOutput(new ObjectResult(result) { StatusCode = StatusCodes.Status400BadRequest });
    }

    public override void OnError(List<Exception> exceptions, CreateUserInput input)
    {
        var result = new ResultApi(input, exceptions.ToStringList());

        SetViewOutput(new ObjectResult(result) { StatusCode = StatusCodes.Status500InternalServerError });
    }
}
