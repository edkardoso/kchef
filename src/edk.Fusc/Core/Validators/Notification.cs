using edk.Fusc.Contracts;
using edk.Fusc.Contracts.Common;

namespace edk.Fusc.Core.Validators;

public class Notification : INotification
{
    public Notification() {
    
    }

    private Notification(string code, string message, SeverityType severity)
    {
        Code = code;
        Message = message;
        Severity = severity;
    }

    public string Code { get; init; } = String.Empty;
    public string Message { get; init; } = String.Empty;
    public SeverityType Severity { get; init; }

    public static Notification ErrorException(string message, string code = "") => new(code, message, SeverityType.Exception);
    public static Notification Error(string message, string code = "") => new(code, message, SeverityType.Error);
    public static Notification Warning(string message, string code = "") => new(code, message, SeverityType.Warning);
    public static Notification Info(string message, string code = "") => new(code, message, SeverityType.Info);

}
