namespace edk.Fusc.Core;

public static class EvaluateLibrary
{

    public static bool Eval(this Func<bool> condition, Action actionTrue, Action actionFalse)
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

    public static bool Eval(this bool condition, Action actionTrue, Action actionFalse) => Eval(() => condition, actionTrue, actionFalse);
    public static bool WhenTrue(this Func<bool> condition, Action actionTrue) => Eval(condition, actionTrue, () => { });
    public static bool WhenTrue(this bool value, Action actionTrue) => Eval(() => value, actionTrue, () => { });
    public static Task<bool> WhenTrueAsync(this bool value, Action actionTrue) => Task.Run(() => Eval(()=> value, actionTrue, () => { }));
    public static bool WhenFalse(this Func<bool> condition, Action actionFalse) => Eval(condition, () => { }, actionFalse);
    public static bool WhenFalse(this bool value, Action actionFalse) => Eval(() => value, () => { }, actionFalse);
    public static bool WhenNotNull(this object obj, Action action) => WhenTrue(obj != null, action);
    public static bool WhenNotNull<T>(this T obj, Action<T> action) => WhenTrue(obj != null, () => { action.Invoke(obj); });
    public static bool WhenNull(this object obj, Action action) => WhenTrue(obj == null, action);
    public static bool WhenGreaterThan(this int value, int valueTarget, Action action) => WhenTrue(value > valueTarget, action);
    public static bool WhenGreaterThanOrEqual(this int value, int valueTarget, Action action) => WhenTrue(value >= valueTarget, action);
    public static bool WhenLessThan(this int value, int valueTarget, Action action) => WhenTrue(value < valueTarget, action);
    public static bool WhenLessThanOrEqual(this int value, int valueTarget, Action action) => WhenTrue(value <= valueTarget, action);
    public static bool WhenEqual(this object obj, object objTarget, Action action) => WhenTrue(obj == objTarget, action);
    public static bool And(params bool[] values) => values.All(element => element);
    public static bool Or(params bool[] values) => values.Contains(true);
}
