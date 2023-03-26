using System.Linq.Expressions;

namespace edk.Tools.NoIf;

public static class NoIf
{
    public static bool If(this Func<bool> condition, Action actionTrue, Action actionFalse)
    {
        var result = condition.Invoke();
        (result ? actionTrue : actionFalse)();
        return result;
    }

    public static TReturn If<T, TReturn>(this T obj
        , Expression<Func<T, bool>> condition
        , Func<T, TReturn> whenTrue
        , Func<T, TReturn> whenFalse) where T : class
        => (condition.Compile()(obj) ? whenTrue : whenFalse)(obj);

    public static T If<T>(this bool condition
        , Func<T> whenTrue
        , Func<T> whenFalse)
        => condition ? whenTrue() : whenFalse();

    public static bool If(this bool condition
        , Action whenTrue
        , Action whenFalse)
        => If(() => condition, whenTrue, whenFalse);

    public static bool? If(this bool? value
       , Action whenTrue
       , Action whenFalse
       , Action whenNull)
    {
        var condition = value.HasValue;

        condition.If(whenTrue: () => value!.Value.If(whenTrue, whenFalse)
            , whenFalse: whenNull);

        return condition;

    }

}


