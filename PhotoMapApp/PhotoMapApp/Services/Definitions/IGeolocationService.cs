using System;
using Plugin.Geolocator;
using Xamarin.Forms.Maps;

namespace PhotoMapApp.Services.Definitions
{
    public interface IGeolocationService
    {
        System.Threading.Tasks.Task<Position> GetCurrentPositionAsync();
    }
}
