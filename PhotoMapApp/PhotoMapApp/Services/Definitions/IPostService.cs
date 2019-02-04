using PhotoMapApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoMapApp.Services.Definitions
{
    interface IPostService
    {
        List<Post> getPosts();
        Post getPost(int id);
        void createPost(string name, string description, List<Tag> tags, string image);
    }
}
