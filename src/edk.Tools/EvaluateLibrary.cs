namespace edk.Tools;

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

    public static T Eval<T>(this bool condition, Func<T> whenTrue, Func<T> whenFalse) => condition ? whenTrue() : whenFalse();
    public static bool Eval(this bool condition, Action whenTrue, Action whenFalse) => Eval(() => condition, whenTrue, whenFalse);
    public static bool WhenTrue(this Func<bool> condition, Action actionTrue) => condition.Eval(actionTrue, () => { });
    public static bool WhenTrue(this bool value, Action actionTrue) => Eval(() => value, actionTrue, () => { });
    public static bool WhenIsTypeEqual<T>(this object obj, Action actionTrue) => (obj.GetType() == typeof(T)).WhenTrue(actionTrue);
    public static Task<bool> WhenTrueAsync(this bool value, Action actionTrue) => Task.Run(() => Eval(() => value, actionTrue, () => { }));
    public static bool WhenFalse(this Func<bool> condition, Action actionFalse) => condition.Eval(() => { }, actionFalse);
    public static bool WhenFalse(this bool value, Action actionFalse) => Eval(() => value, () => { }, actionFalse);
    public static bool WhenNotNull(this object obj, Action action) => (obj != null).WhenTrue(action);
    public static bool WhenNotNull<T>(this T obj, Action<T> action) => (obj != null).WhenTrue(() => action.Invoke(obj));
    public static bool WhenEmpty(this Guid obj, Action action) => (obj == Guid.Empty).WhenTrue(() => action.Invoke());
    public static bool WhenNoEmpty(this Guid obj, Action action) => (obj != Guid.Empty).WhenTrue(() => action.Invoke());
    public static bool WhenNull(this object obj, Action action) => (obj == null).WhenTrue(action);
    public static bool WhenGreaterThan(this int value, int valueTarget, Action action) => (value > valueTarget).WhenTrue(action);
    public static bool WhenGreaterThanOrEqual(this int value, int valueTarget, Action action) => (value >= valueTarget).WhenTrue(action);
    public static bool WhenLessThan(this int value, int valueTarget, Action action) => (value < valueTarget).WhenTrue(action);
    public static bool IsLessThan(this int value, int valueTarget) => value < valueTarget;
    public static bool IsLessThanOrEqual(this int value, int valueTarget) => value <= valueTarget;
    public static bool IsGreaterThan(this int value, int valueTarget) => value > valueTarget;
    public static bool IsGreaterThanOrEqual(this int value, int valueTarget) => value >= valueTarget;
    public static bool WhenLessThanOrEqual(this int value, int valueTarget, Action action) => (value <= valueTarget).WhenTrue(action);
    public static bool WhenEqual(this object obj, object objTarget, Action action) => (obj == objTarget).WhenTrue(action);
    public static bool And(params bool[] values) => values.All(element => element);
    public static bool Or(params bool[] values) => values.Contains(true);
    public static bool Not(this bool value) => !value;

   
}
