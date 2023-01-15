public interface IOption<T>
{
    bool IsNull { get; }
    bool NotIsNull { get; }

    T GetValue();
    TR Match<TR>(Func<T, TR> some, Func<TR> none);
}