using System;
using UserNotifications;

namespace Intouch.Edm.iOS
{
    internal class UserNotificationCenterDelegate : UNUserNotificationCenterDelegate
    {
        public UserNotificationCenterDelegate()
        {
        }

        public override void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
        {
            Console.WriteLine("Active Notification: {0}", notification);
            completionHandler(UNNotificationPresentationOptions.Alert);
        }
    }
}