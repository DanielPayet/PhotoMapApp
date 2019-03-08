using System;
using PhotoMapApp.Services.Definitions;
using Plugin.Geolocator;
using Xamarin.Forms.Maps;
using System.Threading.Tasks;

namespace PhotoMapApp.Services.Implementations
{
    public class GeolocationService : IGeolocationService
    {
        public async Task<Position> GetCurrentPosition()
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
