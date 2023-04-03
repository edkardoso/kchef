using edk.Tools.NoIf.Boolean;

namespace edk.Tools.NoIf.Comparer;

public static class NoIfComparer
{
    public static bool IfIsTypeEqual<T>(this object obj
        , Action actionTrue)
        => (obj.GetType() == typeof(T)).IfTrue(actionTrue);

    public static TResult IfIsTypeEqual<T, TResult>(this object obj
       , Func<TResult> whenTrue) 
        => (obj.GetType() == typeof(T)).If(whenTrue, () => default);

    public static bool IfIsTypeNotEqual<T>(this object obj
        , Action actionTrue)
        => (obj.GetType() != typeof(T)).IfTrue(actionTrue);

    public static bool IfEqual(this object obj
        , object objTarget, Action action)
        => (obj == objTarget).IfTrue(action);

    public static bool IfNotNull(this object obj
        , Action action)
        => (obj != null).IfTrue(action);

    public static bool IfNotNull<T>(this T obj
        , Action<T> action) where T : class
        => (obj != null).IfTrue(() => action.Invoke(obj!));

    public static bool IfEmpty(this Guid obj
        , Action action)
        => (obj == Guid.Empty).IfTrue(() => action.Invoke());

    public static bool IfNotEmpty(this Guid obj
        , Action action)
        => (obj != Guid.Empty).IfTrue(() => action.Invoke());

    public static bool IfNull(this object obj
        , Action action)
        => (obj == null).IfTrue(action);

}
