using System;
using System.Threading;
using System.Threading.Tasks;
using edk.Fusc.Core;

namespace edk.Kchef.Application.Features.Users.Create;

public class CreateUserUseCase : UseCase<CreateUserInput, UserOutput>
{
    protected override string NameUseCase => "CreateUserUseCase";

    public override Task<UserOutput> OnExecuteAsync(CreateUserInput input, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}