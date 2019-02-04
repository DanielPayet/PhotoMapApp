using System;
using Xamarin.Forms;

namespace PhotoMapApp.Services.Definitions
{
    public interface IImageService
    {
        ImageSource GetSource(string name);
    }
}
