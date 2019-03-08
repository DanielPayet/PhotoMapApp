using System;
using PhotoMapApp.Services.Definitions;
using Plugin.Geolocator;
using Xamarin.Forms.Maps;

namespace PhotoMapApp.Services.Implementations
{
    public class GeolocationService : IGeolocationService
    {
        public async System.Threading.Tasks.Task<Position> GetCurrentPositionAsync()
        {

            if (!CrossGeolocator.IsSupported || !CrossGeolocator.Current.IsGeolocationEnabled || !CrossGeolocator.Current.IsGeolocationAvailable)
            {
                return new Position();
            }
            else
            {
                var geolocatorPosition = await CrossGeolocator.Current.GetPositionAsync();
                return new Position(geolocatorPosition.Latitude, geolocatorPosition.Longitude);
            }

        }
    }
}
