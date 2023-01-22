namespace edk.Fusc.Core.Validators;

public static class NotificationCollectionExtension
{
    public static bool NoErrors(this IEnumerable<Notification> notifications)
        => !notifications.HasError();

    public static bool HasError(this IEnumerable<Notification> notifications)
        => notifications.Any(n => n.Severity.Equals(SeverityType.Error));

    public static bool HasWarning(this IEnumerable<Notification> notifications)
        => notifications.Any(n => n.Severity.Equals(SeverityType.Warning));

    public static bool HasInfo(this IEnumerable<Notification> notifications)
       => notifications.Any(n => n.Severity.Equals(SeverityType.Info));

}