namespace edk.Fusc.Core;

public struct NoValue
{
   
    public NoValue(){}

    public static NoValue Instance => new();

    public bool IsNull => true;

}
