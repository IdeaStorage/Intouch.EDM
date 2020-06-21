using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Intouch.Edm.Droid.Services;
using Intouch.Edm.Models;
using Intouch.Edm.Services;
using System.Threading.Tasks;
using Xamarin.Forms;
using Location = Android.Locations.Location;

[assembly: Dependency(typeof(LocationService))]
namespace Intouch.Edm.Droid.Services
{
    public class LocationService : Java.Lang.Object, ILocationService, ILocationListener
    {
        TaskCompletionSource<Location> _tcs;

        public async Task<GeoCoords> GetGeoCoordinatesAsync()
        {
            //Forms.Context
            var manager = (LocationManager)Forms.Context.GetSystemService(Context.LocationService);
           
            if (manager.IsProviderEnabled(LocationManager.GpsProvider) == false)
            {
                return new GeoCoords
                {
                   IsGpsClose = true
                };
                // Intent gpsSettingIntent = new Intent(Android.Provider.Settings.ActionLocat‌​ionSourceSettings);
                // Forms.Context.StartActivity(gpsSettingIntent);
            }
            _tcs = new TaskCompletionSource<Location>();

            manager.RequestSingleUpdate("gps", this, null);

            var location = await _tcs.Task;

            return new GeoCoords
            {
                Latitude = location.Latitude,
                Longitude = location.Longitude
            };
        }

        public void OnLocationChanged(Location location)
        {
            _tcs.TrySetResult(location);
        }

        public void OnProviderDisabled(string provider)
        {
        }

        public void OnProviderEnabled(string provider)
        {
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
        }
    }
}