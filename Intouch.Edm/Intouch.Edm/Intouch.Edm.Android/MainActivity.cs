using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Locations;
using Android.OS;
using Android.Util;
using Firebase.Iid;
using ImageCircle.Forms.Plugin.Droid;
using Plugin.CurrentActivity;
using Plugin.Permissions;
using Xamarin.Forms;

namespace Intouch.Edm.Droid
{
    [Activity(Label = "Acil Durum", Icon = "@drawable/mainIcon", Theme = "@style/MainTheme", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        internal static readonly string CHANNEL_ID = "my_notification_channel";
        internal static readonly int NOTIFICATION_ID = 100;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            ImageCircleRenderer.Init();

            Log.Debug("TOKEN", "Instance token Id:" + FirebaseInstanceId.Instance.Token);
            Helpers.Settings.FirebaseNotification = FirebaseInstanceId.Instance.Token;

            bool isNotification = false;

            //LocationManager locationManager = (LocationManager)Forms.Context.GetSystemService(Context.LocationService);

            //if (locationManager.IsProviderEnabled(LocationManager.GpsProvider) == false)
            //{
            //    Intent gpsSettingIntent = new Intent(Android.Provider.Settings.ActionLocat‌​ionSourceSettings);
            //    Forms.Context.StartActivity(gpsSettingIntent);
            //}

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
        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }
        public void OnActivityResumed(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }
        public void OnActivityStarted(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        protected override void OnStart()
        {
            base.OnStart();
            if (CheckSelfPermission(Manifest.Permission.AccessCoarseLocation) != (int)Permission.Granted)
            {
                RequestPermissions(new string[] { Manifest.Permission.AccessCoarseLocation, Manifest.Permission.AccessFineLocation }, 0);
            }

        }
    }
}