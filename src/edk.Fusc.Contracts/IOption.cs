namespace edk.Fusc.Contracts;
public interface IOption<T>
{
    bool IsNull { get; }
    bool NotIsNull { get; }

    T GetValueOrDefault(T valueDefault);
    TR Match<TR>(Func<T, TR> some, Func<TR> none);
}