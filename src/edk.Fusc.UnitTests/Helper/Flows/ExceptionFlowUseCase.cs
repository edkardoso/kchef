using edk.Fusc.Core;
using edk.Fusc.Core.Mediator;

namespace edk.Fusc.UnitTests.Helper.Flows;

internal class ExceptionFlowUseCase : SuccessFlowUseCase
{
    public override Task<List<string>> OnExecuteAsync(NoValue input, CancellationToken cancellationToken)
    {
        Methods.Add(ActionMethodsName.OnExecuteAsync);

        throw new Exception("Erro forçado para teste de fluxo");

    }

    protected override bool OnActionException(Exception exception, NoValue input, IUser user)
    {
        Methods.Add(ActionMethodsName.OnActionException);

        return base.OnActionException(exception, input, user);
    }
}
