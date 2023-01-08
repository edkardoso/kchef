namespace edk.Kchef.Domain.Common.Fusc
{
    public interface IUseCase
    {
    }
    public interface IUseCase<TInput, TOutput> : IUseCase
    {
        void Handler(IUseCase<TInput, TOutput> other);
    }
}
