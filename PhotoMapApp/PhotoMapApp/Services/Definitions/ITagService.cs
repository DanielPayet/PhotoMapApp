using PhotoMapApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoMapApp.Services.Definitions
{
    public interface ITagService
    {
        List<Tag> GetTags();
        Tag GetTag(int id);
    }
}
