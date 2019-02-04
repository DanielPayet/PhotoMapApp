using System;
using Xamarin.Forms;
using PhotoMapApp.Services.Definitions;

namespace PhotoMapApp.Services.Implementations
{
    public class ImageService : IImageService
    {
        public ImageService()
        {}

        public ImageSource GetSource(string name)
        {
            return ImageSource.FromResource("PhotoMapApp.Resources." + name);
        }
    }
}
