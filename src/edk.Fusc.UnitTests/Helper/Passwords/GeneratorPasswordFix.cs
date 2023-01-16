using edk.Fusc.Core;

namespace edk.Fusc.UnitTests.Helper.Passwords;

internal class GeneratorPasswordFix : UseCase<NoValue, string>
{
    protected override string NameUseCase => "GeneratorPasswordFix";

    public override Task<string> OnExecuteAsync(NoValue input, CancellationToken cancellationToken)
    => Password.Generate();

}

