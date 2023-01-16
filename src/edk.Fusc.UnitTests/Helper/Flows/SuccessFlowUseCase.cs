using edk.Fusc.Core;
using edk.Fusc.Core.Mediator;
using edk.Fusc.Core.Validators;

namespace edk.Fusc.UnitTests.Helper.Flows;

internal class SuccessFlowUseCase : UseCase<NoValue, List<string>>
{
    public List<string> Methods { get; protected set; }

    protected override string NameUseCase => "SuccessFlowUseCase";

    public SuccessFlowUseCase()
    {
        Methods = new();
    }

    protected override void OnActionBeforeStart(NoValue input, IUser user) 
        => Methods.Add(ActionMethodsName.OnActionBeforeStart);

    public override Task<List<string>> OnExecuteAsync(NoValue input, CancellationToken cancellationToken)
    {
        Methods.Add(ActionMethodsName.OnExecuteAsync);

        return Task.FromResult(Methods);
    }

    protected override void OnActionComplete(bool completed, List<Notification> notifications) 
        => Methods.Add(ActionMethodsName.OnActionComplete);
}