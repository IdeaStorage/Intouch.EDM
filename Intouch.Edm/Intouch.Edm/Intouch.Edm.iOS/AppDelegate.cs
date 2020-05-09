using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;
using Firebase.CloudMessaging;
using UserNotifications;

namespace Intouch.Edm.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, IUNUserNotificationCenterDelegate , IMessagingDelegate
    {
        public event EventHandler<UserInfoEventArgs> MessageReceived;
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());
            RegisterForRemoteNotifications();
            Messaging.SharedInstance.Delegate = this;
            if (UNUserNotificationCenter.Current != null)
            {
                UNUserNotificationCenter.Current.Delegate = new UserNotificationCenterDelegate();
            }
            Firebase.Core.App.Configure();

            return base.FinishedLaunching(app, options);
        }
        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))

            {
                // For iOS 10 display notification (sent via APNS)

                UNUserNotificationCenter.Current.Delegate = this;

                var authOptions = UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound;

                UNUserNotificationCenter.Current.RequestAuthorization(authOptions, async (granted, error) =>

                {

                    Console.WriteLine(granted);

                    await System.Threading.Tasks.Task.Delay(500);

                });

            }
            else
            {
                // iOS 9 or before

                var allNotificationTypes = UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound;

                var settings = UIUserNotificationSettings.GetSettingsForTypes(allNotificationTypes, null);

                UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);

            }
            UIApplication.SharedApplication.RegisterForRemoteNotifications();

            Messaging.SharedInstance.ShouldEstablishDirectChannel = true;
        }
        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            Messaging.SharedInstance.ApnsToken = deviceToken;
        }

        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {
            base.FailedToRegisterForRemoteNotifications(application, error);
        }
        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            completionHandler(UIBackgroundFetchResult.NewData);
        }
        [Export("messaging: didReceiveRegistrationToken:")]
        public void DidReceiveRegistrationToken(Messaging messaging, string fcmToken)

        {
            Xamarin.Forms.Application.Current.Properties["Fcmtocken"] = Messaging.SharedInstance.FcmToken ?? "";

            Xamarin.Forms.Application.Current.SavePropertiesAsync();

            System.Diagnostics.Debug.WriteLine($"######Token######  :  {fcmToken}");

            Console.WriteLine(fcmToken);
            Helpers.Settings.FirebaseNotification = token;
        }
    }
}
