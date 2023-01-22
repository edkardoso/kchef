namespace edk.Fusc.Core.Validators
{
    public struct Notification
    {

        public string Message { get; init; }
        public SeverityType Severity { get; init; }


        public static Notification Error(string message) => new() { Message = message, Severity = SeverityType.Error };
        public static Notification Warning(string message) => new() { Message = message, Severity = SeverityType.Warning };
        public static Notification Info(string message) => new() { Message = message, Severity = SeverityType.Info };

      

    }
}
