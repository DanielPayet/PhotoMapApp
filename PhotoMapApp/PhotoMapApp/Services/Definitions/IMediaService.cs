using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhotoMapApp.Services.Definitions
{
    public interface IMediaService
    {
        Task<string> getImage();
    }
}
