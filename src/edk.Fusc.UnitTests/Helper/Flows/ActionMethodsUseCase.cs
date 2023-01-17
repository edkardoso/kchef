using edk.Fusc.Core;
using edk.Fusc.Core.Mediator;
using edk.Fusc.Core.Validators;

namespace edk.Fusc.UnitTests.Helper.Flows;

internal class BeforeStartUseCase : UseCase<NoValue, bool>
{

    protected override string NameUseCase => throw new NotImplementedException();

    protected override bool OnActionBeforeStart(NoValue input, IUser user)
    {
        SetNotification(ActionMethodsName.OnActionBeforeStart, SeverityType.Info);

        return false;
    }

    public override Task<bool> OnExecuteAsync(NoValue input, CancellationToken cancellationToken)
    {
        SetNotification(ActionMethodsName.OnExecuteAsync, SeverityType.Info);

        return Task.FromResult(false);

    }

    protected override bool OnActionComplete(bool completed, IReadOnlyCollection<Notification> notifications)
    {
        SetNotification(ActionMethodsName.OnActionComplete, SeverityType.Info);


        return base.OnActionComplete(completed, notifications);
    }

}

