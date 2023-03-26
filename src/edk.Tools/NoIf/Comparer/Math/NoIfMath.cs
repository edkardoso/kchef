using edk.Tools.NoIf.Boolean;
using edk.Tools.NoIf.Miscellaneous;

namespace edk.Tools.NoIf.Comparer.Math;

public static class NoIfMath
{
    public static bool IfGreaterThan(this int value
       , int valueTarget, Action action)
       => value.IsGreaterThan(valueTarget).IfTrue(action);

    public static bool IfGreaterThanOrEqual(this int value
        , int valueTarget
        , Action action) => value.IsGreaterThanOrEqual(valueTarget).IfTrue(action);

    public static bool IfLessThan(this int value
        , int valueTarget
        , Action action) => value.IsLessThan(valueTarget).IfTrue(action);

    public static bool IfLessThanOrEqual(this int value
        , int valueTarget
        , Action action) => value.IsLessThanOrEqual(valueTarget).IfTrue(action);

    public static void IfLessOrEqualOrGreater(this int value
        , int valueTarget
        , Action whenLess
        , Action whenEqual
        , Action whenGreater)
    {

        if (value.Equals(valueTarget))
        {
            whenEqual();
        }
        else
        {
            (value.IsGreaterThan(valueTarget) ? whenGreater : whenLess)();
        }
    }
}
