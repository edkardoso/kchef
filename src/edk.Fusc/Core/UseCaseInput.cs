using edk.Fusc.Contracts;

namespace edk.Fusc.Core;

public abstract class UseCaseInput<TInput> : UseCase<TInput, NoValue>
{
    protected UseCaseInput()
    {

    }
    protected UseCaseInput(IMediatorUseCase mediator) : base(mediator)
    {

    }
}
