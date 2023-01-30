namespace edk.Fusc.Core;

public static class BoolExtension
{

    public static bool If(this Func<bool> condition, Action actionTrue, Action actionFalse)
    {
        var result = condition.Invoke();

        if (result)
        {
            actionTrue();
        }
        else
        {
            actionFalse();
        }

        return result;

    }

    public static bool If(this bool condition, Action actionTrue, Action actionFalse) => If(() => condition, actionTrue, actionFalse);
    public static bool IfTrue(this Func<bool> condition, Action actionTrue) => If(condition, actionTrue, () => { });
    public static Task<bool> IfTrueAsync(this Func<bool> condition, Action actionTrue) => Task.Run(() => If(condition, actionTrue, () => { }));
    public static bool IfTrue(this bool value, Action actionTrue) => If(() => value, actionTrue, () => { });
    public static bool IfFalse(this Func<bool> condition, Action actionFalse) => If(condition, () => { }, actionFalse);
    public static bool IfFalse(this bool value, Action actionFalse) => If(() => value, () => { }, actionFalse);
    public static bool IfNotNull(this object obj, Action action) => IfTrue(obj != null, action);
    public static bool IfNotNull<T>(this T obj, Action<T> action) => IfTrue(obj != null, () => { action.Invoke(obj); });
    public static bool IfNull(this object obj, Action action) => IfTrue(obj == null, action);
    public static bool IfGreaterThan(this int value, int valueTarget, Action action) => IfTrue(value > valueTarget, action);
    public static bool IfGreaterThanOrEqual(this int value, int valueTarget, Action action) => IfTrue(value >= valueTarget, action);
    public static bool IfLessThan(this int value, int valueTarget, Action action) => IfTrue(value < valueTarget, action);
    public static bool IfLessThanOrEqual(this int value, int valueTarget, Action action) => IfTrue(value <= valueTarget, action);
    public static bool IfEqual(this object obj, object objTarget, Action action) => IfTrue(obj == objTarget, action);
    public static bool And(params bool[] values) => values.All(element => element);
    public static bool Or(params bool[] values) => values.Contains(true);
}
