using System.Data;
using edk.Fusc.Core;
using edk.Fusc.Core.Mediator;
using edk.Fusc.Core.Validators;
using FluentValidation;

namespace edk.Fusc.UnitTests.Helper.Flows;


internal class ErrorFlowValidator : AbstractValidator<int>
{
    public ErrorFlowValidator()
    {
        RuleFor(r => r).GreaterThan(0);
    }
}

internal class ErrorFlowUseCase : UseCase<int, List<string>>
{

    public ErrorFlowUseCase(ErrorFlowValidator validator)
        : base(validator: validator)
    {

    }

    public List<string> Methods { get; protected set; } = new List<string>();

    protected override string NameUseCase => "ErrorFlowUseCase";



    protected override bool OnActionBeforeStart(int input, IUser user)
    {
        Methods.Add(ActionMethodsName.OnActionBeforeStart);
        return base.OnActionBeforeStart(input, user);
    }

    public override Task<List<string>> OnExecuteAsync(int input, CancellationToken cancellationToken)
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