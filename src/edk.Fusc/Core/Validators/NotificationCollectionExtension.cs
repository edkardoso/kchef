using edk.Fusc.Contracts;
using edk.Fusc.Contracts.Common;

namespace edk.Fusc.Core.Validators;

public static class NotificationCollectionExtension
{
    public static bool NoErrors(this IEnumerable<INotification> notifications)
        => !notifications.HasError();

    public static bool HasError(this IEnumerable<INotification> notifications)
        => notifications.Any(n => n.Severity.Equals(SeverityType.Error));

    public static bool HasWarning(this IEnumerable<INotification> notifications)
        => notifications.Any(n => n.Severity.Equals(SeverityType.Warning));

    public static bool HasInfo(this IEnumerable<INotification> notifications)
       => notifications.Any(n => n.Severity.Equals(SeverityType.Info));

    public static void AddMany(this IEnumerable<INotification> notifications, IEnumerable<INotification> notificationsNew)
    {
        if (notificationsNew == null || notifications.ToList().Count==0)
            return;

        notifications.ToList().AddRange(notificationsNew);
    }

}