using edk.Fusc.Core;

namespace edk.Fusc.UnitTests.Help.Scenario01;

internal class GeneratorPassword : UseCase<int, string>
{
    protected override string NameUseCase => "GeneratorPassword";

    public override Task<string> OnExecuteAsync(int input, CancellationToken cancellationToken)
        => Password.Generate(input);
}
