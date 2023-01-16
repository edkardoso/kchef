using edk.Fusc.Core;
using Newtonsoft.Json.Linq;

namespace edk.Fusc.UnitTests.Help.Scenario01;

internal class GeneratorPassowordVoidOtherVersion : UseCase<NoValue, NoValue>
{
    public string? Value{ get; private set; }

    protected override string NameUseCase => "GeneratorPassowordVoidOtherVersion";

    public override async Task<NoValue> OnExecuteAsync(NoValue input, CancellationToken cancellationToken)
    {
        Value = await Password.Generate(6);

        return await NoValueTask();
    }
}

