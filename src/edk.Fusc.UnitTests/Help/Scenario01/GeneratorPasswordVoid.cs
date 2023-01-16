using edk.Fusc.Core;

namespace edk.Fusc.UnitTests.Help.Scenario01;

internal class GeneratorPasswordVoid : UseCase<int, NoValue>
{
    public string? Value { get; private set; }

    protected override string NameUseCase => "GeneratorPasswordVoid";

    public override async Task<NoValue> OnExecuteAsync(int input, CancellationToken cancellationToken)
    {
        Value = await Password.Generate(input);

        return await NoValueTask();
    }

   
}

