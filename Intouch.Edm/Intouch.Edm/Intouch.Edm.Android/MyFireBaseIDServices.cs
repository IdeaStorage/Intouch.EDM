using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Firebase.Iid;
using Firebase.Messaging;
using System;
namespace Intouch.Edm.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        const string TAG = "MyFireBaseMessagingService";
        public static string notificationTypeId = "";
        internal static readonly string CHANNEL_ID = "my_notification_channel";
        public override void OnMessageReceived(RemoteMessage message)
        {
            Log.Debug(TAG, "From: " + message.From);

            var body = message.GetNotification().Body;
            Log.Debug(TAG, "Notification Message Body: " + body);
            base.OnMessageReceived(message);
            notificationTypeId = message.Data["NotificationTypeId"];

            try
            {
                SendNotification(message.GetNotification().Body, message.GetNotification().Title);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:>>" + ex);
            }
        }

        private void SendNotification(string body, string header)
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                //var intent = new Intent(this, typeof(MainActivity));
                //intent.AddFlags(ActivityFlags.ClearTop);
                //var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);

                var intent = new Intent("MainActivity");
                intent.AddFlags(ActivityFlags.ClearTop);

                PendingIntent pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.Immutable);


                var notificationBuilder = new Notification.Builder(this)
                            .SetContentTitle(header)
                            .SetSmallIcon(Resource.Drawable.fire)
                            .SetContentText(body)
                            .SetAutoCancel(true)
                            .SetContentIntent(pendingIntent);

                //var notificationManager = NotificationManager.FromContext(this);
                var notificationManager = (Android.App.NotificationManager)this.GetSystemService(Context.NotificationService);
                notificationManager.Notify(1, notificationBuilder.Build());
            }
            else
            {
                var intent = new Intent(this, typeof(MainActivity));
                intent.AddFlags(ActivityFlags.ClearTop);
                var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);

                var notificationBuilder = new Android.App.Notification.Builder(this, CHANNEL_ID)
                            .SetContentTitle(header)
                            .SetSmallIcon(Resource.Drawable.fire)
                            .SetContentText(body)
                            .SetAutoCancel(true)
                            .SetContentIntent(pendingIntent)
                            .SetChannelId(CHANNEL_ID);

                if (Build.VERSION.SdkInt < BuildVersionCodes.O)
                {
                    return;
                }

                var channel = new NotificationChannel(CHANNEL_ID, "FCM Notifications", NotificationImportance.High)
                {
                    Description = "Firebase Cloud Messages appear in this channel"
                };

                var notificationManager = (NotificationManager)GetSystemService(NotificationService);
                notificationManager.CreateNotificationChannel(channel);

                notificationManager.Notify(0, notificationBuilder.Build());
            }
        }
    }

    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class MyFirebaseIIDService : FirebaseInstanceIdService
    {
        const string TAG = "MyFirebaseIIDService";
        public override void OnTokenRefresh()
        {
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            SendRegistrationToServerAsync(refreshedToken);
        }
        public void SendRegistrationToServerAsync(string token)
        {
            Helpers.Settings.FirebaseNotification = token;
            //duyurular yetkisi varsa notifications topic ine subscribe olacak
            FirebaseMessaging.Instance.SubscribeToTopic("notifications");

        }
    }

}