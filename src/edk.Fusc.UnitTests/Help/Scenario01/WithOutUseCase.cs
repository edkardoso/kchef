using edk.Fusc.Core;

namespace edk.Fusc.UnitTests.Help.Scenario01;

internal class WithOutUseCase : UseCase<NoValue, NoValue>
{
    protected override string NameUseCase => "WithOutUseCase";

    public override Task<NoValue> OnExecuteAsync(NoValue input, CancellationToken cancellationToken)
        => Task.FromResult(NoValue.Create);
}

