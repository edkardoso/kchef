using edk.Fusc.Contracts.Common;

namespace edk.Fusc.Contracts;
public interface INotification
{
    string Message {  get; init; }
    SeverityType Severity { get; init; }
}