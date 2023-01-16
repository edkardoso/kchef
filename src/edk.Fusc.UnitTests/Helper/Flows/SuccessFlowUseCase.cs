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

    protected override bool OnActionBeforeStart(NoValue input, IUser user)
    {
        Methods.Add(ActionMethodsName.OnActionBeforeStart);
        return base.OnActionBeforeStart(input, user);
    }

    public override Task<List<string>> OnExecuteAsync(NoValue input, CancellationToken cancellationToken)
    {
        Methods.Add(ActionMethodsName.OnExecuteAsync);

        return Task.FromResult(Methods);
    }

    protected override bool OnActionComplete(bool completed, List<Notification> notifications)
    {
        Methods.Add(ActionMethodsName.OnActionComplete);
        return base.OnActionComplete(completed, notifications); 
    }
}