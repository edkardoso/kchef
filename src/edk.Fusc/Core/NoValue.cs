namespace edk.Fusc.Core;

public struct NoValue
{
    public NoValue(bool isNull = false) => IsNull = isNull;

    public static NoValue Create => new();

    public static NoValue Null => new(true);

    public bool IsNull { get; }
}
