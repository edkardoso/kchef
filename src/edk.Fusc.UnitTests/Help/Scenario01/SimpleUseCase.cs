using edk.Fusc.Core;

namespace edk.Fusc.UnitTests.Help.Scenario01;

internal class SimpleUseCase : UseCase<int, int>
{
    protected override string NameUseCase => "SimpleUseCase";

    public override Task<int> OnExecuteAsync(int input, CancellationToken cancellationToken)
        => Task.FromResult(input * input);
}
