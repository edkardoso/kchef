using edk.Fusc.Core;

namespace edk.Fusc.UnitTests.Helper.Passwords
{
    internal class GeneratorPasswordVoidOtherVersion : UseCase<NoValue, NoValue>
    {
        public string? Value { get; private set; }

        protected override string NameUseCase => "GeneratorPassowordVoidOtherVersion";

        public override async Task<NoValue> OnExecuteAsync(NoValue input, CancellationToken cancellationToken)
        {
            Value = await Password.Generate(6);

            return NoValue.Instance;
        }
    }
}