using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.CurrentActivity;
using ImageCircle.Forms.Plugin.Droid;

namespace Intouch.Edm.Droid
{
    
    [Activity(Label = "Intouch.Edm", Icon = "@mipmap/icon", Theme = "@style/MainTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private object msgText;
        static readonly string TAG = "MainActivity";
        internal static readonly string CHANNEL_ID = "my_notification_channel";
        internal static readonly int NOTIFICATION_ID = 100;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            ImageCircleRenderer.Init();

            bool isNotification = false;

            if (Intent.Extras != null)
            {
                foreach (var key in Intent.Extras.KeySet())
                {
                    var value = Intent.Extras.GetString(key);
                    if (key == "NotificationTypeId")
                    {
                        if (value?.Length > 0)
                        {
                            isNotification = true;
                            LoadApplication(new App(value));
                        }
                    }
                }
            }
            ////Foreground mode
            //if (FirebaseNotificationService.webContentList.ToString() != "")
            //{
            //    isNotification = true;
            //    LoadApplication(new App(domainname, FirebaseNotificationService.webContentList.ToString()));
            //    FirebaseNotificationService.webContentList = "";
            //}

            //Normal loading
            if (!isNotification)
            {
                LoadApplication(new App());
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}