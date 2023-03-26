namespace edk.Tools.NoIf.Miscellaneous;

public static class NoIfMiscellaneous
{
    public static bool IfAllTrue(params bool[] values) => values.All(element => element);
    public static bool IfAny(params bool[] values) => values.Contains(true);
    public static bool IfAny(params Func<bool>[] funcs)
    {
        if (funcs is null)
        {
            throw new ArgumentNullException(nameof(funcs));
        }

        foreach (var func in funcs)
        {
            if (func.Invoke())
                return true;
        }

        return false;
    }

    public static bool IsLessThan(this int value, int valueTarget) => value < valueTarget;
    public static bool IsLessThanOrEqual(this int value, int valueTarget) => value <= valueTarget;
    public static bool IsGreaterThan(this int value, int valueTarget) => value > valueTarget;
    public static bool IsGreaterThanOrEqual(this int value, int valueTarget) => value >= valueTarget;
    public static bool Not(this bool value) => !value;
    public static bool IsFalse(this bool value) => !value;
}
