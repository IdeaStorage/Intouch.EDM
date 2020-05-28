using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Util;
using Firebase.Iid;
using ImageCircle.Forms.Plugin.Droid;
using Plugin.CurrentActivity;

namespace Intouch.Edm.Droid
{
    [Activity(Label = "Intouch.Edm", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
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

            Log.Debug("TOKEN", "Instance token Id:" + FirebaseInstanceId.Instance.Token);
            Helpers.Settings.FirebaseNotification = FirebaseInstanceId.Instance.Token;

            bool isNotification = false;

            if (Intent.Extras != null)
            {
                foreach (var key in Intent.Extras.KeySet())
                {
                    var value = Intent.Extras.GetString(key);
                    if (key == "NotificationTypeId" && value?.Length > 0)
                    {
                        isNotification = true;
                        LoadApplication(new App(value));
                    }
                }
            }
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