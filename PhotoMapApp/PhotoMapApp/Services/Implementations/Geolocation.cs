using System;
using PhotoMapApp.Services.Definitions;
using Plugin.Geolocator;
using System.Threading.Tasks;
using Plugin.Geolocator.Abstractions;
using Position = Xamarin.Forms.Maps.Position;
using System.Collections.Generic;
using System.Linq;

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

        public async Task<string> GetAdresseFromPosition(Position position)
        {
            if (!CrossGeolocator.IsSupported || !CrossGeolocator.Current.IsGeolocationEnabled || !CrossGeolocator.Current.IsGeolocationAvailable) {
                return "";
            } else {
                var geolocatorPosition = new Plugin.Geolocator.Abstractions.Position(position.Latitude, position.Longitude);
                Address adresse = ( await CrossGeolocator.Current.GetAddressesForPositionAsync(geolocatorPosition) ).ToList().First();
                return adresse.SubThoroughfare + " " + 
                       adresse.Thoroughfare +  ", " + 
                       adresse.PostalCode + " " +
                       adresse.Locality;
            }
        }
    }
}
