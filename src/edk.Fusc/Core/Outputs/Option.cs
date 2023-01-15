public struct Option<T> : IOption<T>
{
    internal Option(T value)
    {
        Value = value;
    }

    internal T Value { get; }

    public bool IsNull => Value == null;
    public bool NotIsNull => !IsNull;

    public TR Match<TR>(Func<T, TR> some, Func<TR> none)
        => NotIsNull ? some(Value) : none();

    public static readonly Option<T> Null = new();

    public static Option<T> New(T value)
       => new(value);

    public T GetValue()
    {
        if (IsNull)
            throw new ArgumentNullException(nameof(Value));

        return Value;
    }
}
