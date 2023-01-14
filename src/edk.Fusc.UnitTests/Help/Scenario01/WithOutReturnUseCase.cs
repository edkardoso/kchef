using edk.Fusc.Core;

namespace edk.Fusc.UnitTests.Help.Scenario01;

internal class WithOutReturnUseCase : UseCase<int, NoValue>
{
    protected override string NameUseCase => "WithOutReturnUseCase";

    public override Task<NoValue> OnExecuteAsync(int input, CancellationToken cancellationToken)
        => Task.FromResult(NoValue.Create);
}

