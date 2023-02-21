using System;

namespace edk.Tools;

public struct Option<T> : IOption<T>
{
    public Option(T value)
    {
        Value = value;
        Message = "This object is a container. Uses the method 'GetValueOrDefault' or 'Match' to repair your value.";
    }
    public string Message { get;  }
    internal T Value { get; }

    public bool IsNull => Value == null;
    public bool NotNull => !IsNull;

    public TR Match<TR>(Func<T, TR> some, Func<TR> none)
        => NotNull ? some(Value) : none();

    public static readonly Option<T> Null = new();

    public static Option<T> New(T value)
       => new(value);

    public T GetValueOrDefault(T valueDefault)
    {
        if (valueDefault == null)
            throw new ArgumentNullException(nameof(valueDefault));

        return Value ?? valueDefault;
    }
}
