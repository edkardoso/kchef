namespace edk.Tools.NoIf.Boolean;

public static class NoIfBoolean
{

    public static bool IfTrue(this Func<bool> condition
        , Action actionTrue)
        => condition.If(actionTrue, () => { });

    public static bool IfTrue(this bool value
        , Action actionTrue)
        => NoIf.If(() => value, actionTrue, () => { });

    public static bool IfFalse(this Func<bool> condition
        , Action actionFalse)
        => condition.If(() => { }, actionFalse);

    public static bool IfFalse(this bool value
        , Action actionFalse)
        => NoIf.If(() => value, () => { }, actionFalse);

    public static Task<bool> IfTrueAsync(this bool value
        , Action actionTrue)
        => Task.Run(() => NoIf.If(() => value, actionTrue, () => { }));

}
