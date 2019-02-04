using PhotoMapApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoMapApp.Services.Definitions
{
    interface ITagService
    {
        List<Tag> getTags();
        Tag getTag(int id);
    }
}
