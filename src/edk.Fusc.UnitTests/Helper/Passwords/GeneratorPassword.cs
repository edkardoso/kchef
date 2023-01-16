using edk.Fusc.Core;

namespace edk.Fusc.UnitTests.Helper.Passwords;

internal class GeneratorPassoword : UseCase<int, string>
{
    protected override string NameUseCase => "GeneratorPassoword";

    public override Task<string> OnExecuteAsync(int input, CancellationToken cancellationToken)
        => Password.Generate(input);
}
