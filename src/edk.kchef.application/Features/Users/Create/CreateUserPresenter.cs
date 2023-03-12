using edk.Fusc.Contracts;
using edk.Fusc.Core.Presenters;
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
        SetViewOutput(new ObjectResult(output) { StatusCode = StatusCodes.Status201Created });
    }

    public override void OnErrorValidation(CreateUserInput input, IReadOnlyCollection<INotification> notifications)
    {
        SetViewOutput(new ObjectResult(notifications) { StatusCode = StatusCodes.Status400BadRequest });
    }

    public override void OnError(List<Exception> exceptions, CreateUserInput input)
    {
        SetViewOutput(new ObjectResult(exceptions) { StatusCode = StatusCodes.Status500InternalServerError });
    }
}
