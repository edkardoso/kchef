namespace edk.Tools.Common;

public interface IOption<T>
{
    bool IsNull { get; }
    bool NotNull { get; }

    T GetValueOrDefault(T valueDefault);
    TR Match<TR>(Func<T, TR> some, Func<TR> none);
}