using System;
using System.Collections.Generic;
using PhotoMapApp.Models;

namespace PhotoMapApp
{
    public interface IDatabaseService
    {
        List<Post> GetPosts();
        void UpdateOrSave(Post post);
        void Delete(Post post);

        List<Tag> GetTags();
        void UpdateOrSave(Tag tag);
        void Delete(Tag tag);
    }
}
