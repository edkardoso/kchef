using edk.Fusc.Core;

namespace edk.Fusc.UnitTests.Help.Scenario01;

internal class WithOutInputUseCase : UseCase<NoValue, int>
{
    protected override string NameUseCase => "WithOutInputUseCase";

    public override Task<int> OnExecuteAsync(NoValue input, CancellationToken cancellationToken)
        => Task.FromResult(0);
}

