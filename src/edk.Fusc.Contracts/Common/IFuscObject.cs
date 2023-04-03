using System.Data.SqlTypes;

namespace edk.Fusc.Contracts.Common;

public interface INullableObject
{
    public bool IsNull() => true;
}

public interface IFuscObject 
{
    
}
