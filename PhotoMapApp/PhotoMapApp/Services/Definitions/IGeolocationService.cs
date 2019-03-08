using System;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace PhotoMapApp.Services.Definitions
{
    public interface IGeolocationService
    {
        Task<Position> GetCurrentPosition();
        Task<string> GetAdresseFromPosition(Position position);
    }
}
