using edk.Fusc.Core;

namespace edk.Fusc.UnitTests.Help.Scenario01;

internal class GeneratorPasswordFix : UseCase<NoValue, string>
{
    protected override string NameUseCase => "GeneratorPasswordFix";

    public override Task<string> OnExecuteAsync(NoValue input, CancellationToken cancellationToken)
    => Password.Generate();

}

